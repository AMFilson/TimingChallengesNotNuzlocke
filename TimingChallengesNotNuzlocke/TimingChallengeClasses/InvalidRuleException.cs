// Filename: InvalidRuleException
// Author: Kyle Chapman
// Date: 2026-11-23
// DESC: A custom class for validating randomized game challenge rules
//       copied and cited by Andrew Filson for TimingChallenges program


using System;
using System.Collections.Generic;
using System.Text;

namespace TimingChallengesNotNuzlocke.TimingChallengeClasses
{
    internal class InvalidRuleException : Exception
    {
        string message;

        /// <summary>
        /// Creates a new InvalidRuleException.
        /// </summary>
        /// <param name="message">The reason that the rule is invalid.</param>
        public InvalidRuleException(string messageValue) : base(messageValue)
        {
            message = "Invalid Rule: " + messageValue;
        }

        /// <summary>
        /// Returns the message from the InvalidRuleException.
        /// </summary>
        public override string Message => message;
    }
}
