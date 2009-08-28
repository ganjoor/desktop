using System;
using System.Collections.Generic;
using System.Text;

namespace ganjoor
{
    public delegate void PageChangedEvent(string PageString, bool CanGoNextPoem, bool CanGoPreviousPoem, bool HasComments, bool CanBrowse, string highlightedText, int ItemsFound);
}
