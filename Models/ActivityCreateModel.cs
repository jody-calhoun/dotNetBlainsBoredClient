using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace dotNetBlainsBoredClient.Models
{
    public class ActivityCreateModel
    {

        public ActivityModel? Activity { get; set; }

        public List<string>? ActivityTypes { get; set; }

        public List<ActivityModel>? listActivities { get; set; }

    }
}
