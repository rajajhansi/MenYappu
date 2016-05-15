using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RjamSoft.Tamil.Grammar.Parser.Tests
{
    public class KutriyalikaramTests
    {
        [Fact]
        public void TestIfKutiyalikaramsAreParsedCorrectly()
        {
            var kurals = new string[]
            {
                @"அஃகாமை செல்வத்திற் கியாதெனின் வெஃகாமை
வேண்டும் பிறன்கைப் பொருள்",
                @"அருளல்ல தியாதெனிற் கொல்லாமை கோறல்
பொருளல்ல தவ்வூன் றினல்",
                @"வாய்மை யெனப்படுவ தியாதெனின் யாதொன்றுந்
தீமை யிலாத சொலல்",
                @"நல்லா றெனப்படுவ தியாதெனின் யாதொன்றுங்
கொல்லாமை சூழு நெறி",
                @"கடாஅ வுருவொடு கண்ணஞ்சா தியாண்டு
முகாஅமை வல்லதே யொற்று",
                @"நயந்தவர்க்கு நல்காமை நேர்ந்தேன் பசந்தவென்
பண்பியார்க் குரைக்கோ பிற",
                @"துன்பத்திற் கியாரே துணையாவார் தாமுடைய
நெஞ்சந் துணையல் வழி",
                @"இன்மையி னின்னாத தியாதெனி னின்மையி
னின்மையே யின்னா தது",
                @"பழைமை எனப்படுவது யாதெனின் யாதும்
கிழமையைக் கீழ்ந்திடா நட்பு",
                @"வெண்மை எனப்படுவ தியாதெனின் ஒண்மை
உடையம்யாம் என்னும் செருக்கு",
                @"யாண்டுச்சென்று யாண்டும் உளராகார் வெந்துப்பின்
வேந்து செறப்பட் டவர்"
            };

            var prosodyParser = new ProsodyParser("", true, false);
            var wrongThirukkurals = new List<string>();
            foreach (var kural in kurals)
            {
                try
                {
                    var prosodyPart =  prosodyParser.Parse(kural);
                    if (!prosodyPart.MetreType.Contains("வெண்பா"))
                    {
                        wrongThirukkurals.Add(kural);
                    }
                }
                catch (Exception e)
                {
                    wrongThirukkurals.Add(kural);
                }
            }
            var wrongKutriyalikaramFile = @"InputFiles\wrongKutriyalikaram.txt";
            File.WriteAllLines(wrongKutriyalikaramFile, wrongThirukkurals);
        }

        [Fact]
        public void TestIfTheseKuralsParseCorrectlyAfterApplyingPunarchiRules()
        {
            var kurals = new string[]
            {
                @"அன்பீனும் ஆர்வம் உடைமை அதுவீனும்
நண்பென்னும் நாடாச் சிறப்பு",
                @"அறத்திற்கே அன்புசார் பென்ப அறியார்
மறத்திற்கும் அஃதே துணை"
            };
            var prosodyParser = new ProsodyParser("", true, false);
            var wrongThirukkurals = new List<string>();
            foreach (var kural in kurals)
            {
                try
                {
                    var prosodyPart = prosodyParser.Parse(kural);
                    if (!prosodyPart.MetreType.Contains("வெண்பா"))
                    {
                        wrongThirukkurals.Add(kural);
                    }
                }
                catch (Exception e)
                {
                    wrongThirukkurals.Add(kural);
                }
            }
            var wrongKutriyalikaramFile = @"InputFiles\wrongKuralsAfterPunarchiRules.txt";
            File.WriteAllLines(wrongKutriyalikaramFile, wrongThirukkurals);
        }
    }
}
