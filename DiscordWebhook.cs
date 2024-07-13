using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public static class DiscordWebhook
{
    public static bool AllowWebhook = false; // <-- Change to "true" to allow webhook
    public static string WebhookURL = "your_webhook_url"; // <-- Webhook Url from Discord
    public static string WebhookURL2 = "your_webhook_url2"; // <-- For Additional Webhooks
    public static string UserID = "your_user_id"; // <-- Can be used to easily manage pinged users
    public static string UserID2 = "your_user_id2";
    public static void SendMs(string message, string webhook)
    {
        WebClient client = new WebClient();
        client.Headers.Add("Content-Type", "application/json");
        string payload = "{\"content\": \"" + message + "\"}";
        client.UploadData(webhook, Encoding.UTF8.GetBytes(payload));
    }

}


// USAGE
//
//  if (DiscordWebhook.AllowWebhook == true)
//  {
//      DiscordWebhook.SendMs($"{exeFile}: OFFLINE", DiscordWebhook.WebhookURL);
//  }
//
//
//  You can name the WebhookURL string whatever you want to make it easier