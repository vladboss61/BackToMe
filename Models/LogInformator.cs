namespace BackToMe.Models
{
    using System;
    using System.Text;
    using BackToMe.Interfaces;
    public class LogInformator
    {
        private const string exceptionError = "Invalid builder";
        private StringBuilder _build = new StringBuilder(String.Empty);
        public LogInformator FromSource(string nameOfController)
        {
            _build.Append($" Source : {nameOfController} ");
            return this; 
        }
        public LogInformator FromOperation(string nameOfOperation)
        {
            _build.Append($" Operation : {nameOfOperation} ");
            return this;
        }
        public LogInformator Information(string logInformation)
        {
            _build.Append($" {logInformation} ");
            return this;
        }
        public string Build()
        {
            var buildString = _build?.ToString();
            return !string.IsNullOrEmpty(buildString) ? buildString : throw new InvalidOperationException(exceptionError);            
        }        
    } 

}