using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Dialog
{
    public class DialogManager
    {
        /// <summary>
        /// Gets the response sets.
        /// </summary>
        /// <value>The response sets.</value>
        public Dictionary<string, Response[]> ResponseSets { get; private set; }

        /// <summary>
        /// Gets the prompts.
        /// </summary>
        /// <value>The prompts.</value>
        public Dictionary<string, Prompt> Prompts { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Dialog.DialogManager"/> class.
        /// </summary>
        private DialogManager() {

        }

        /// <summary>
        /// Creates a new DialogManager based on the data stored in the XML file.
        /// </summary>
        /// <returns>The new DialogManager</returns>
        /// <param name="monsterId">Monster identifier.</param>
        public static DialogManager LoadFromXml(Guid monsterId) {
            var xmlAsset = new TextAsset();
            xmlAsset = (TextAsset)Resources.Load("Data/MonsterDialog.xml", typeof(TextAsset));

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlAsset.text);

            var monsterDialogData = xmlDoc.GetElementById(monsterId.ToString());
            var dialogManager = new DialogManager();

            dialogManager.Prompts = new Dictionary<string, Prompt>();
            foreach (XmlNode xmlNode in monsterDialogData.GetElementsByTagName("prompt")) {
                dialogManager.Prompts[xmlNode.Attributes["id"].Value] = new Prompt
                {
                    Body = xmlNode.InnerText,
                    NextPromptID = xmlNode.Attributes["nextPrompt"].Value,
                    ResponseSetID = xmlNode.Attributes["responseSet"].Value
                };
            }

            dialogManager.ResponseSets = new Dictionary<string, Response[]>();
            foreach (XmlNode xmlNode in monsterDialogData.GetElementsByTagName("responseSet")) {
                var responses = new List<Response>();
                foreach (XmlNode responseNode in xmlNode.ChildNodes) {
                    responses.Add(new Response
                    {
                        Body = responseNode.InnerText,
                        Value = int.Parse(responseNode.Attributes["value"].Value),
                        NextPromptID = responseNode.Attributes["nextPrompt"].Value
                    });
                }
                dialogManager.ResponseSets.Add(xmlNode.Attributes["id"].Value, responses.ToArray());
            }

            return dialogManager;
        }
    }
}