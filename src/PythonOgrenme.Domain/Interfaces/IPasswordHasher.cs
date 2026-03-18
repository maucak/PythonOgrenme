namespace PythonOgrenme.Domain.Interfaces;

public interface IPasswordHasher
{
    string Hash(string sifre);
    bool Verify(string sifre, string hash);
}