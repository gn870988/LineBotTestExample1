using isRock.LineBot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ExampleLineBot20170928.Controllers
{

    public class LineBotController : ApiController
    {
        [HttpPost]
        [Signature]
        public IHttpActionResult POST()
        {
            string ChannelAccessToken = Properties.Settings.Default.ChannelAccessToken;

            try
            {
                string postData = Request.Content.ReadAsStringAsync().Result;
                var ReceivedMessage = Utility.Parsing(postData);
                Bot bot = new Bot(ChannelAccessToken);
                string Lineid = ReceivedMessage.events.FirstOrDefault().source.userId;

                // 1.【事件類別】
                //---------------------------------------------------------
                string CommonFieldsType = ReceivedMessage.events.FirstOrDefault().source.type;
                string EventType = ReceivedMessage.events.FirstOrDefault().type;
                string MessageEventType = ReceivedMessage.events.FirstOrDefault().message.type;

                bot.PushMessage(Lineid, $"CommonFields：{CommonFieldsType}");
                bot.PushMessage(Lineid, $"EventType：{EventType}");
                bot.PushMessage(Lineid, $"MessageEvent：{MessageEventType}");
                //---------------------------------------------------------
                // 2.【上傳圖片】
                //if(ReceivedMessage.events.FirstOrDefault().message.type =="image")
                //{
                //    string ContentID = ReceivedMessage.events.FirstOrDefault().message.id.ToString();
                //    // 取得使用者bytedata
                //    byte[] filebody = Utility.GetUserUploadedContent(ContentID, ChannelAccessToken);
                //    string filename = $"/image/{Guid.NewGuid()}.png";
                //    var path = HttpContext.Current.Request.MapPath(filename);
                //    File.WriteAllBytes(path, filebody);

                //    bot.PushMessage(Lineid, "您上傳圖片為下方：");
                //    bot.PushMessage(Lineid, new Uri($"https://{HttpContext.Current.Request.Url.Host}{filename}"));
                //}
                //---------------------------------------------------------
                // 3.【DatetimePickerAction】
                //---------------------------------------------------------
                //var Template = new List<TemplateActionBase>();

                //Template.Add(new DateTimePickerAction()
                //{
                //    label = "test", // 標籤文字
                //    mode = "date", // 有三個mode：date、time、datetime
                //    data = "Postbackdata", // Postback資料
                //    initial = "2017-09-28", // 時間初始值
                //    max = "2017-12-31", // 時間最大值
                //    min = "2017-01-01" // 時間最小值
                //});

                //var ButtonsTemplate = new ButtonsTemplate()
                //{
                //    thumbnailImageUrl = new Uri("https://pics.iclope.com/news/test-cigarette-electronique.jpg"),
                //    title = "標題",
                //    text = "內容文字",
                //    altText = "當不支援裝置的文字",
                //    actions = Template
                //};

                //// 接收Postback資料
                //if (ReceivedMessage.events.FirstOrDefault().type == "postback")
                //{
                //    //判斷data為"Postbackdata"
                //    if (ReceivedMessage.events.FirstOrDefault().postback.data == "Postbackdata")
                //    {
                //        // 使用者選擇時間
                //        string Date = ReceivedMessage.events.FirstOrDefault().postback.Params.date;
                //        bot.PushMessage(Lineid, $"您選擇日期為{Date}");
                //        return Ok();
                //    }
                //}

                //bot.PushMessage(Lineid, ButtonsTemplate);
                //---------------------------------------------------------
            }
            catch (Exception ex)
            {
                // 錯誤通知
                Bot bot = new Bot(ChannelAccessToken);
                bot.PushMessage("YourLineID", ex.Message);
            }

            return Ok();
        }
    }
}