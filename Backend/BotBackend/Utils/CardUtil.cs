using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using AdaptiveCards;
using CognitiveServicesBot.Model;
using Microsoft.Bot.Connector;

namespace LuisBot.Utils
{
    public static class CardUtil
    {
        public const string ContosoLogoUrl = "contosologo.png";
        public static Attachment CreateCardAttachment(string channelID, Value value)
        {
            Attachment attachment = null;
            switch (channelID)
            {
                case "skype":
                    attachment = CreateThumbnailCard(value).ToAttachment();
                    break;
                default:
                    attachment = new Attachment()
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = CreateFeatureCard(value)
                    };
                    break;
            }
            return attachment;
        }

        public static ThumbnailCard CreateThumbnailCard(Value value)
        {
            var card = new ThumbnailCard();
            card.Title = value.name;
            card.Subtitle = value.type + " / " + value.status;
            card.Images = new List<CardImage>()
            {
                new CardImage(string.Format(WebConfigurationManager.AppSettings["BotCardsBlobStorageURL"], ContosoLogoUrl))
            };
            card.Text = value.details;

            //If an action to be introduced to every job result, here is where to put it
            //card.Buttons = new List<CardAction>()
            //{
            //    new CardAction()
            //    {
            //        Value = value.id,
            //        Type = ActionTypes.OpenUrl,
            //        Title = "View Job"
            //    }
            //};
            
            return card;
        }

        public static AdaptiveCard CreateFeatureCard(Value value)
        {
            AdaptiveCard card = new AdaptiveCard();
            card.Speak = value.name;
            card.Body = new List<CardElement> {
                    new ColumnSet
                    {
                        Columns =
                        {
                            new Column
                            {
                                Size = ColumnSize.Auto,
                                Items =
                                {
                                    new Image
                                    {
                                        //Url = string.Format(ContosoImagesBlob, ContosoLogoUrl),
                                        Url = string.Format(WebConfigurationManager.AppSettings["BotCardsBlobStorageURL"], ContosoLogoUrl),
                                        Size = ImageSize.Small,
                                        Style = ImageStyle.Normal
                                    }
                                }
                            },
                            new Column
                            {
                                Size = ColumnSize.Stretch,
                                Items =
                                {
                                    new TextBlock
                                    {
                                        Text = value.name,
                                        Weight = TextWeight.Bolder,
                                        Size = TextSize.Large
                                    }
                                }
                            }
                        }
                    },
                    new Container
                    {
                        Items =
                        {
                            new TextBlock
                            {
                                Text = value.details,
                                Speak = value.details,
                                Wrap = true
                            },
                            new FactSet
                            {
                                Facts =
                                {
                                    new AdaptiveCards.Fact{Title = "Type", Value = value.type},
                                    new AdaptiveCards.Fact{Title = "Status", Value = value.status}
                                }
                            }
                        }
                    }
                };
            //card.Actions = new List<ActionBase>
            //{
            //    new OpenUrlAction{Title = "View Job", Url=value.id}
            //};

            return card;
        }
    }
}
