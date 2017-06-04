using System;
using FindIt.Models;

namespace FindIt.ViewModels
{
    public class ChallengeViewModel
    {
        public Guid ChallengeId { get; set; }
        public Guid SendingUserId { get; set; }
        public Guid ReceivingUserId { get; set; }
        public Guid GameId { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }

        public ChallengeViewModel(Challenge model)
        {
            ChallengeId = model.ChallengeId;
            SendingUserId = model.SendingUserId;
            ReceivingUserId = model.ReceivingUserId;
            GameId = model.GameId;
            Message = model.Message;
            Seen = model.Seen;
        }
    }
}