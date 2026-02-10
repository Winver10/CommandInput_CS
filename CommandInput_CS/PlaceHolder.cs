using System;

namespace CommandPlaceHolder_CS;

static public class PlaceHolders
{
    static public string PlaceHolderForSpace = $"PLACEHOLDER_{Guid.NewGuid()}_FOR_SPACE";
    static public string PlaceHolderForMoreAction = $"PLACEHOLDER_{Guid.NewGuid()}_FOR_MORE_ACTION";
}
