using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Dialog
{
    public class DialogDataManager
    {
        /// <summary>
        /// The path to the data file for monster dialog.
        /// </summary>
        private static string _MONSTER_DIALOG_STREAMING_ASSETS_FILE_PATH = "Data/MonsterDialog.xml";

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
        /// Gets the initial prompt ID.
        /// </summary>
        public string InitialPromptID { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Dialog.DialogDataManager"/> class.
        /// </summary>
        private DialogDataManager() {

        }

        /// <summary>
        /// Creates a new DialogDataManager based on the data stored in the XML file.
        /// </summary>
        /// <returns>The new DialogDataManager</returns>
        /// <param name="monsterId">Monster identifier.</param>
        public static DialogDataManager LoadFromXml(Guid monsterId) {
            var xmlFilePath = Path.Combine(Application.streamingAssetsPath, _MONSTER_DIALOG_STREAMING_ASSETS_FILE_PATH);
            var xmlFileContents = File.ReadAllText(xmlFilePath);
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlFileContents);

            var monsterDialogData = xmlDoc.GetElementById(monsterId.ToString("B").ToUpper());
            var dialogDataManager = new DialogDataManager
            {
                InitialPromptID = monsterDialogData.Attributes["initialDialog"].Value,
                Prompts = new Dictionary<string, Prompt>(),
                ResponseSets = new Dictionary<string, Response[]>()
            };

            foreach (XmlNode xmlNode in monsterDialogData.GetElementsByTagName("prompt")) {
                var prompt = new Prompt
                {
                    Body = xmlNode.InnerText.Trim()
                };

                if (xmlNode.Attributes["nextPrompt"] != null)
                {
                    prompt.NextPromptID = xmlNode.Attributes["nextPrompt"].Value;
                }

                if (xmlNode.Attributes["responseSet"] != null)
                {
                    prompt.ResponseSetID = xmlNode.Attributes["responseSet"].Value;
                }

                if (xmlNode.Attributes["isSaidByPlayer"] != null && bool.Parse(xmlNode.Attributes["isSaidByPlayer"].Value))
                {
                    prompt.IsSaidByPlayer = true;
                }

                if (xmlNode.Attributes["animState"] != null )
                {
                    prompt.AnimState = (DialogAnimState) Enum.Parse(typeof(DialogAnimState), xmlNode.Attributes["animState"].Value);
                }

                dialogDataManager.Prompts[xmlNode.Attributes["id"].Value] = prompt;
            }
            
            foreach (XmlNode xmlNode in monsterDialogData.GetElementsByTagName("responseSet")) {
                var responses = new List<Response>();
                foreach (XmlNode responseNode in xmlNode.ChildNodes) {
                    responses.Add(new Response
                    {
                        Body = responseNode.InnerText.Trim(),
                        Value = int.Parse(responseNode.Attributes["value"].Value),
                        NextPromptID = responseNode.Attributes["nextPrompt"].Value
                    });
                }
                dialogDataManager.ResponseSets.Add(xmlNode.Attributes["id"].Value, responses.ToArray());
            }

            return dialogDataManager;
        }
    }
}