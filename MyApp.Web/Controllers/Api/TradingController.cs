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
        private readonly IConfiguration _configuration;
        private readonly List<TelegramConfig> _telegrams;

        public TradingController(IConfiguration configuration)
        {
            this._configuration = configuration;
            _telegrams = configuration.GetSection("Telegrams").Get<List<TelegramConfig>>();
        }
        [HttpPost("CryptoSpot")]
        public IActionResult Crypto([FromBody] InforCandle candle)
        {
            var signal = candle.signal;
            var ticker = candle.ticker;
            var close = candle.close;
            var time = candle.time;
            var msg = "<b>Signal BUY/SELL spot</b>\n" + signal + " #" + ticker + "\n";
            msg = msg + " - Price: <b>" + close + "</b>\n";
            msg = msg + " - Time UTC: <em>" + time + "</em>\n";
            msg = msg + " - Time GMT+7:<em>" + TimeHelper.ConvertToTimeVN(time) + "</em>\n";

            foreach(var telegram in _telegrams)
            {
                if(telegram.CanSend)
                    SendTelegramMessage(telegram.Token,telegram.ChatId, msg);
            }
            
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
                    text = message,
                    parse_mode = "HTML"
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
    public class TelegramConfig
    {
        public string ChatId { get; set; }
        public string Token { get; set; }
        public bool CanSend { get; set; }
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
    public class TimeHelper {
        public static string ConvertToTimeVN(string utcTimeString)
        {
            DateTime utcTime = DateTime.Parse(utcTimeString, null, System.Globalization.DateTimeStyles.RoundtripKind);
            TimeZoneInfo gmtPlus7 = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime gmtPlus7Time = TimeZoneInfo.ConvertTimeFromUtc(utcTime, gmtPlus7);
            string formattedTime = gmtPlus7Time.ToString("dd-MM-yyyy HH:mm:ss");
            return formattedTime;
        }
    }
}