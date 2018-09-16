namespace BackToMe.Models
{
    using System;
    using System.Text;
    using Interfaces;

    public sealed class LogInformationBuilder : ILogBuilder
    {
        private const string ExceptionError = "Invalid builder";

        private readonly StringBuilder _build = new StringBuilder(string.Empty);

        public ILogBuilder FromSource(string nameOfSource)
        {
            _build.Append($" Source : {nameOfSource} ");
            return this; 
        }
        public ILogBuilder FromOperation(string nameOfOperation)
        {
            _build.Append($" Operation : {nameOfOperation} ");
            return this;
        }
        public ILogBuilder Information(string logInformation)
        {
            _build.Append($" {logInformation} ");
            return this;
        }
        public string Build()
        {
            var buildString = _build?.ToString();
            return !string.IsNullOrEmpty(buildString) ? buildString : throw new InvalidOperationException(ExceptionError);            
        }        
    } 
}