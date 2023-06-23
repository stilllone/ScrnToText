using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrnToText.Interface
{
    public interface ILanguageService
    {
        void SetLanguage(string lang);
        string GetCurrentLanguage();
    }
}
