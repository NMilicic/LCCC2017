using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FindIt.Models;

namespace FindIt.ViewModels
{
    public class PostGameViewModel
    {
        public double Score { get; set; }
        public List<Achievements> NewAchievements { get; set; }
    }
}