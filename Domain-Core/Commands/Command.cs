using System;
using Domain_Core.Events;
using FluentValidation.Results;

namespace Domain_Core.Commands 
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

         public abstract bool IsValid();
    }
}