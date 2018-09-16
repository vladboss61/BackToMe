namespace BackToMe.Interfaces
{
    public interface ILogBuilder
    {
        ILogBuilder FromSource(string nameOfSource);

        ILogBuilder FromOperation(string nameOfOperation);

        ILogBuilder Information(string logInformation);

        string Build();
    }
}
