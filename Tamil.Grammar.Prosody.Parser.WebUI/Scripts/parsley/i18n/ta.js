// This is included with the Parsley library itself,
// thus there is no use in adding it to your project.
//import Parsley from '../parsley/main';

Parsley.addMessages('en', {
  defaultMessage: "உள்ளிட்ட மதிப்பு தவறானதாகத் தெரிகிறது.",
  type: {
    email:        "சரியான மின்னஞ்சலை உள்ளிடவும்.",
    url:          "சரியான் வலை முகவரியை உள்ளிடவும்",
    number:       "சரியான எண்ணை உள்ளிடவும்",
    integer:      "சரியான முழு எண்ணை உள்ளிடவும்",
    digits:       "சரியான இலக்கங்களை உள்ளிடவும்",
    alphanum:     "சரியான எண்ணெழுத்தை உள்ளிடவும்."
  },
  notblank:       "காலியான உள்ளீடாய் இருக்கக்கூடாது",
  required:       "இது தேவையான் உள்ளீடு.",
  pattern:        "உள்ளிட்ட மதிப்பு தவறானதாகத் தெரிகிறது.",
  min:            "உள்ளிட்ட மதிப்பு %s-க்கும் சமமான அல்லது அதிகமான மதிப்பாய் இருக்க வேண்டும்.",
  max:            "உள்ளிட்ட மதிப்பு %s-க்கும் சமமான அல்லது குறைவான மதிப்பாய் இருக்க வேண்டும்.",
  range:          "உள்ளிட்ட மதிப்பு %s-க்கும்  %s-க்கும் இடைப்பட்ட மதிப்பாய் இருக்க வேண்டும்.This value should be between %s and %s.",
  minlength:      "உள்ளிட்ட மதிப்பு மிகவும் குறைவு. குறைந்தது %s எழுத்துகள் அல்லது அதற்கும் அதிகமாய் இருக்க வேண்டும்.",
  maxlength:      "உள்ளிட்ட மதிப்பு மிகவும் நீளம்.  அதிகபட்சம் %s எழுத்துகள் அல்லது அதற்கும் குறைவாய் இருக்க வேண்டும்.",
  length:         "உள்ளிட்ட மதிப்பின் எண்ணிக்கை தவறாகும். %s-க்கும்  %s-க்கும் இடைப்பட்ட எண்ணிக்கையில் எழுத்துகள் இருக்க வேண்டும்",
  mincheck:       "குறைந்தபட்சம் %s தேர்வுகள் செய்யவேண்டும்.",
  maxcheck:       "கட்டாயமாக %s அல்லது அதற்கும் குறைவான தேர்வுகள் செய்யவேண்டும்.",
  check:          "கட்டாயமாக %s-க்கும்  %s-க்கும் இடைப்பட்ட தேர்வுகள் செய்யவேண்டும்.",
  equalto:        "இந்த மதிப்பு சமமாக இருக்க வேண்டும்."
});

Parsley.setLocale('en');