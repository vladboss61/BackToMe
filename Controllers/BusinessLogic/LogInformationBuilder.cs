namespace BackToMe.Models
{
    using System;
    using System.Text;
    using Interfaces;

    public sealed class LogInformationBuilder : ILogBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public ILogBuilder FromSource(string nameOfSource)
        {
            _stringBuilder.Append($" Source : {nameOfSource} ");
            return this; 
        }
        public ILogBuilder FromOperation(string nameOfOperation)
        {
            _stringBuilder.Append($" Operation : {nameOfOperation} ");
            return this;
        }
        public ILogBuilder Information(string logInformation)
        {
            _stringBuilder.Append($" {logInformation} ");
            return this;
        }
        public string Build()
        {            
            return _stringBuilder.ToString();
        }        
    } 
}