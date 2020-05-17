using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OrderChallange
{
    public class ChallengesOrderHandle : MonoBehaviour
    {
        private static Text[] challangeNames;

        private static int challangeToChange;
        private static int challangeFromChange;
        private static bool changing;

        void Start()
        {
            challangeNames = GetComponentsInChildren<Text>();
            changing = false;
        }

        public static void SetChallangeClick(int challange)
        {
            if (!changing)
            {
                challangeToChange = challange - 1;
                changing = true;
                challangeNames[challangeToChange].GetComponentInParent<Image>().color = Color.grey;
            }
            else
            {
                challangeFromChange = challange - 1;
                ChangeChallenges();
                changing = false;
                challangeNames[challangeToChange].GetComponentInParent<Image>().color = Color.white;
            }
        }

        private static void ChangeChallenges()
        {
            string aux = challangeNames[challangeFromChange].text;
            challangeNames[challangeFromChange].text = challangeNames[challangeToChange].text;
            challangeNames[challangeToChange].text = aux;
        }
    }
}
