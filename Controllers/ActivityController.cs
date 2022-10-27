using dotNetBlainsBoredClient.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace dotNetBlainsBoredClient.Controllers
{
    public class ActivityController : Controller
    {

        private readonly ILogger<ActivityController> _logger;

        public ActivityController(ILogger<ActivityController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string baseEndpoint = "https://www.boredapi.com/api/activity/";
            List<ActivityModel>? listActivities = new List<ActivityModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseEndpoint);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage resActivities = await client.GetAsync(baseEndpoint);
                if (resActivities.IsSuccessStatusCode)
                {
                    listActivities = JsonConvert.DeserializeObject<List<ActivityModel>>("[" + resActivities.Content.ReadAsStringAsync().Result + "]");
                }
                return View(listActivities);
            }
        }

        public async Task<ActionResult> Create(ActivityCreateModel queryActivity)
        {      
            // Create model
            ActivityCreateModel viewModel = new ActivityCreateModel();
             
                // Load select list items for activity types
                viewModel.Activity = new ActivityModel();
                viewModel.ActivityTypes = new List<string>()
                    {
                        "busywork",
                        "charity",
                        "cooking", 
                        "diy",
                        "education",
                        "music",
                        "recreational",
                        "relaxation",
                        "social"
                    };

                // Check for null
                if (queryActivity.Activity != null)
                {

                    // Validation after submit
                    if (!ModelState.IsValid)
                    {
                        return View(viewModel);
                    }
                    
                    // Api call
                    const string url = "https://www.boredapi.com/api/activity";
                    Dictionary<string, string> queryString = new Dictionary<string, string>() {
                        { "type", queryActivity.Activity.type },
                        { "participants", queryActivity.Activity.participants.ToString() },
                        { "price", queryActivity.Activity.price.ToString() },
                        { "accessibility", queryActivity.Activity.accessibility.ToString() }
                    };
                    Uri urlEndpoint = new Uri(QueryHelpers.AddQueryString(url, queryString));

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = urlEndpoint;
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage resActivities = await client.GetAsync(urlEndpoint);
                        if (resActivities.IsSuccessStatusCode)
                        {
                            List<ActivityModel>? listActivities = JsonConvert.DeserializeObject<List<ActivityModel>>("[" + resActivities.Content.ReadAsStringAsync().Result + "]");
                            if (listActivities.Any())
                            {
                                List<ActivityModel> currentlistActivities = new List<ActivityModel>();
                                foreach (var activity in listActivities)
                                {
                                    currentlistActivities.Add(activity);
                                }
                                viewModel.listActivities = currentlistActivities;
                            }
                        }
                    }

                }
            return View(viewModel);            
        }

    }
}