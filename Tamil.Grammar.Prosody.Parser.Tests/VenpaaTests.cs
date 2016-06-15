using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RjamSoft.Tamil.Grammar.Prosody.Parser;
using Xunit;
namespace RjamSoft.Tamil.Grammar.Parser.Tests
{  
    public class VenpaaTests
    {
        [Fact]
        public void TestIfVenpaIsOruVikarpaKuralVenpa()
        {
            var kural = @"முற்ற உணர்ந்தானை ஏத்தி மொழிகுவன்
குற்றமொன்று இல்லா அறம்";

            var prosodyParser = new ProsodyParser(kural);
            prosodyParser.Parse();

            Assert.Equal("ஒரு விகற்ப குறள் வெண்பா", prosodyParser.MetreType);
        }

        [Fact]
        public void FactIfVenpaIsIruVikarpaKuralVenpa()
        {
            var kural = @"நற்காட்சி நன்ஞானம் நல்லொழுக்கம் இம்மூன்றும்
தொக்க அறச்சொல் பொருள்";

            var prosodyParser = new ProsodyParser(kural);
            prosodyParser.Parse();

            Assert.Equal("இரு விகற்ப குறள் வெண்பா", prosodyParser.MetreType);
        }

        [Fact]
        public void TestIfVenpaIsOruVikarpaInnisaiSinthiyalVenpa()
        {
            var kural = @"தேர்வெதற்கு பிள்ளைகளைப் பள்ளிகளில் சேர்ப்பதற்கே
தேர்விலைஊ ழல்அரசி யல்வாதி ஆள்வதற்கே
தீர்வதுகற் றோர்நாடாள் தல்";

            var prosodyParser = new ProsodyParser(kural);
            prosodyParser.Parse();

            Assert.Equal("ஒரு விகற்ப இன்னிசைச் சிந்தியல் வெண்பா", prosodyParser.MetreType);
        }

        [Fact]
        public void TestIfVenpaIsOruVikarpaNerisaiSinthiyalVenpa()
        {
            var kural = @"அறிந்தானை ஏத்தி அறிவாங் கறிந்து
செறிந்தார்க்குச் செவ்வன் உரைப்ப - செறிந்தார்
சிறந்தமை ஆராய்ந்து கொண்டு";

            var prosodyParser = new ProsodyParser(kural);
            prosodyParser.Parse();

            Assert.Equal("ஒரு விகற்ப நேரிசைச் சிந்தியல் வெண்பா", prosodyParser.MetreType);
        }
        [Fact]
        public void TestIfVenpaIsIruVikarpaInnisaiSinthiyalVenpa()
        {
            var kural = @"தன்னல மற்றுன் நலத்திற்கு ழைத்திட்ட
உன்னத ஆசான்க ளையென்றும் உந்தனிருக்
கண்களினும் மேலாகப் போற்று";

            var prosodyParser = new ProsodyParser(kural);
            prosodyParser.Parse();

            Assert.Equal("இரு விகற்ப இன்னிசைச் சிந்தியல் வெண்பா", prosodyParser.MetreType);
        }

        [Fact]
        public void TestIfVenpaIsIruVikarpaNerisaiSinthiyalVenpa()
        {
            var kural = @"தேர்வெதற்கு பிள்ளைகளைப் பள்ளிகளில் சேர்ப்பதற்கே
தேர்விலை நம்மையா ள்வோருக்கே-- தீர்வது
கற்றவ ராட்சிசெய் தல்";

            var prosodyParser = new ProsodyParser(kural);
            prosodyParser.Parse();

            Assert.Equal("இரு விகற்ப நேரிசைச் சிந்தியல் வெண்பா", prosodyParser.MetreType);
        }
        [Fact]
        public void TestIfVenpaIsPalaVikarpaInnisaiSinthiyalVenpa()
        {
            var kural = @"தன்னல மற்றுன் நலத்திற்கு ழைத்திட்ட
ஆசான்க யென்றுமே உன்னதம்  உந்தனிருக்
கண்களினும் மேலாகப் போற்று";

            var prosodyParser = new ProsodyParser(kural);
            prosodyParser.Parse();

            Assert.Equal("பல விகற்ப இன்னிசைச் சிந்தியல் வெண்பா", prosodyParser.MetreType);
        }

        [Fact]
        public void TestIfASinthiyalNerisaiVenpaCannotBeAPalaVikarpaNerisaiSinthiyalVenpa()
        {
            var kural = @"தேர்வெதற்கு பிள்ளைகளைப் பள்ளிகளில் சேர்ப்பதற்கே
தேர்விலை நம்மையா ள்வோருக்கே-- தீர்வது
கற்றவ ராட்சிசெய் தல்";

            var prosodyParser = new ProsodyParser(kural);
            prosodyParser.Parse();

            Assert.NotEqual("பல விகற்ப நேரிசைச் சிந்தியல் வெண்பா", prosodyParser.MetreType);
        }
        [Fact]
        public void TestIfVenPaIsPalaVikarpaPahrodaiVenpa()
        {
            var venpa = @"இனத்தை அழித்த இலங்கை அரக்கனை
இன்முகம் காட்டி இளித்தல் இழிவு
இனியும் பொறுத்தால் இறையாண்மை என்பது
இன்னும் இருக்கிற தாவென்று கேட்போம்
மனிதம் மறந்ததோ மத்ய ப்ரதேசம்
புனிதமான புத்தமதக் கல்வி நிலையம்
துவக்கிட வேறு புனிதரில் லையோ 
உவக்காது புண்பட்ட எங்கள் மனது
கருணை சிறிதுமற்றக் காலன் அவனைத்
திருப்பி அனுப்பி டுக";

            var prosodyParser = new ProsodyParser(venpa);
            prosodyParser.Parse();

            Assert.Equal("பல விகற்ப பஃறொடை வெண்பா", prosodyParser.MetreType);
        }
        [Fact]
        public void TestIfVenPaIsPalaVikarpaPahrodaiVenpa2()
        {
            var venpa = @"ஓட்டுத்தே வைக்காய் தமிழுணர் வைத்தூண்டி
விட்டு உருப்படாத வேலைசெய் யும்வெற்று
வேட்டுகள் ஐய்யா  மரவெட்டி அம்மாவே
வீட்டுக்குள் ளேயிருந்து வீணாய் அறிக்கைகள்
விட்ட சரவைக்கும் வாய்ச்சொல்லில் வீரர்காள்
ஓட்டுகள் வங்கியில் லாவுணர் வாளராம்
ரோட்டுக்கு வந்தேனும் போராடும் வைகோக்காண்
ஓட்டுகளால் வேட்டுகள் வைத்தால் பதவியை
விட்டோ டிடுவா ரவர்";

            var prosodyParser = new ProsodyParser(venpa);
            prosodyParser.Parse();

            Assert.Equal("பல விகற்ப பஃறொடை வெண்பா", prosodyParser.MetreType);
        }
    }
}
