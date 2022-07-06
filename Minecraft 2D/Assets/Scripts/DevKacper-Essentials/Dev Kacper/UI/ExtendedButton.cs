using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DevKacper.UI
{
    public class ExtendedButton : UnityEngine.UI.Button
    {
        private TextMeshProUGUI text;
        public string Text
        {
            get { return GetText().text; }
            set { GetText().text = value; }
        }

        public TextMeshProUGUI GetText()
        {
            if(text == null)
            {
                text = GetComponentInChildren<TextMeshProUGUI>();
            }

            return text;
        }
    }
}
