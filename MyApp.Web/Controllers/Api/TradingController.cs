using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;

namespace MyApp.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingController : ControllerBase
    {
        [HttpPost("CryptoSpot")]
        public IActionResult Crypto([FromBody] InforCandle candle)
        {
            var signal = candle.signal;
            var ticker = candle.ticker;
            var close = candle.close;
            var time = candle.time;
            var msg = "Signal BUY/SELL spot\n" + signal + " #" + ticker + "\n";
            msg = msg + " - Price: " + close + "\n";
            msg = msg + " - Time: " + time + "\n";
            msg = msg + " - Version 2";

            //TOKEN id of bot chat telegram
            string token = "7457090210:AAHFDTBhpux_v-NoD44YVhK1LgmxJ3Lp4YU";
            string chatId = "-1002451917277_1";

            SendTelegramMessage(token, chatId, msg);

            var r = new ResultTradingView { status = "success" };
            return Ok(r);
        }
       

        static async Task SendTelegramMessage(string token, string chatId, string message)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.telegram.org/bot{token}/sendMessage";

                var payload = new
                {
                    chat_id = chatId,
                    text = message
                };

                var jsonPayload = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(payload),
                    Encoding.UTF8,
                    "application/json"
                );

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, jsonPayload);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending message: {ex.Message}");
                }
            }
        }

    }
    public class InforCandle
    {
        public string signal { get; set; } 
        public string ticker { get; set; }
        public string close { get; set; }
        public string time { get; set; }

    }
    class ResultTradingView
    {
        public string status { get; set; }
    }
}