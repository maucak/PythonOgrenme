using MediatR;
using PythonOgrenme.Application.Commands.Answers;
using PythonOgrenme.Application.DTOs;
using PythonOgrenme.Domain.Entities;
using PythonOgrenme.Domain.Events;
using PythonOgrenme.Domain.Interfaces;

namespace PythonOgrenme.Application.Handlers.Answers;

public class SubmitAnswerCommandHandler
    : IRequestHandler<SubmitAnswerCommand, SubmitAnswerResult>
{
    private const int DOGRU_CEVAP_PUANI = 10;
    private const int MAX_DENEME = 3;

    private readonly IQuestionRepository _questionRepo;
    private readonly IUserRepository _userRepo;
    private readonly IAnswerRepository _answerRepo;
    private readonly IUserStatsRepository _statsRepo;
    private readonly IRabbitMQPublisher _publisher;

    public SubmitAnswerCommandHandler(
        IQuestionRepository questionRepo,
        IUserRepository userRepo,
        IAnswerRepository answerRepo,
        IUserStatsRepository statsRepo,
        IRabbitMQPublisher publisher)
    {
        _questionRepo = questionRepo;
        _userRepo = userRepo;
        _answerRepo = answerRepo;
        _statsRepo = statsRepo;
        _publisher = publisher;
    }

    public async Task<SubmitAnswerResult> Handle(
        SubmitAnswerCommand request, CancellationToken cancellationToken)
    {
        var soru = await _questionRepo.GetByIdAsync(request.SoruId)
            ?? throw new KeyNotFoundException($"Soru bulunamadı: {request.SoruId}");

        var kullanici = await _userRepo.GetByIdAsync(request.KullaniciId)
            ?? throw new KeyNotFoundException($"Kullanıcı bulunamadı: {request.KullaniciId}");

        // Kaçıncı deneme?
        var mevcutDeneme = await _answerRepo.GetDenemeNoAsync(
            request.KullaniciId, request.SoruId);
        var denemeNo = mevcutDeneme + 1;

        // Daha önce doğru çözdüyse tekrar kabul etme
        var oncekiDogruCevap = await _answerRepo.GetByKullaniciVeSoruAsync(
            request.KullaniciId, request.SoruId);
        if (oncekiDogruCevap is { DogruMu: true })
        {
            return new SubmitAnswerResult
            {
                DogruMu = true,
                GeriBildirim = "Bu soruyu daha önce doğru çözdünüz.",
                KazanilanPuan = 0,
                DenemeNo = denemeNo,
                SonDeneme = false
            };
        }

        // Cevabı normalize ederek doğrula
        var dogruMu = soru.CevapDogrula(request.VerilenCevap);

        // Cevabı kaydet
        var answer = new Answer(
            request.KullaniciId, request.SoruId,
            request.VerilenCevap, dogruMu, denemeNo);
        await _answerRepo.AddAsync(answer);
        await _answerRepo.SaveChangesAsync();

        if (dogruMu)
        {
            // Puanı ekle
            kullanici.PuanEkle(DOGRU_CEVAP_PUANI);
            await _userRepo.UpdateAsync(kullanici);
            await _userRepo.SaveChangesAsync();

            // İstatistikleri güncelle
            await IstatistikleriGuncelleAsync(kullanici);

            // Event yayınla → RabbitMQ → SignalR
            await _publisher.PublishAsync(new ScoreUpdatedEvent(
                kullanici.Id, kullanici.KullaniciAdi,
                kullanici.Puan, kullanici.Seviye));

            return new SubmitAnswerResult
            {
                DogruMu = true,
                GeriBildirim = soru.GeriBildirimAciklamasi
                    ?? "Tebrikler! Doğru cevap!",
                KazanilanPuan = DOGRU_CEVAP_PUANI,
                DenemeNo = denemeNo,
                SonDeneme = false
            };
        }

        // Yanlış cevap
        var sonDeneme = denemeNo >= MAX_DENEME;
        return new SubmitAnswerResult
        {
            DogruMu = false,
            GeriBildirim = sonDeneme
                ? $"Son hakkınızı da kullandınız. Doğru cevap: {soru.DogruCevap}"
                : $"Yanlış cevap. {MAX_DENEME - denemeNo} hakkınız kaldı.",
            KazanilanPuan = 0,
            DenemeNo = denemeNo,
            SonDeneme = sonDeneme
        };
    }

    private async Task IstatistikleriGuncelleAsync(Domain.Entities.User kullanici)
    {
        var stats = await _statsRepo.GetByKullaniciIdAsync(kullanici.Id);
        var tamamlanan = await _answerRepo.GetTamamlananSoruIdleriAsync(kullanici.Id);
        var toplamAktif = await _questionRepo.GetTotalActiveCountAsync();

        if (stats == null)
        {
            stats = new UserStats(kullanici.Id);
            stats.Guncelle(kullanici.Puan, tamamlanan.Count(), kullanici.StreakSayisi, toplamAktif);
            await _statsRepo.AddAsync(stats);
        }
        else
        {
            stats.Guncelle(kullanici.Puan, tamamlanan.Count(), kullanici.StreakSayisi, toplamAktif);
            await _statsRepo.UpdateAsync(stats);
        }
        await _statsRepo.SaveChangesAsync();
    }
}