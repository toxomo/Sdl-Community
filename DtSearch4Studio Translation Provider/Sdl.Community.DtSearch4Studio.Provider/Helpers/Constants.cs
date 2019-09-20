namespace Sdl.Community.DtSearch4Studio.Provider.Helpers
{
	public static class Constants
	{
		public static readonly string AppUpperName = "DtSearch4Studio";
		public static readonly string AppLowerName = "dtSearch4Studio";
		public static readonly string NoIndexSelected = "No index was selected";
		public static readonly string ProviderScheme = "dtsearch";
		public static readonly string NoSettingsMessage = "Provider settings cannot be null!";
		public static readonly string JsonPath = @"SDL Community\DtSearch4Studio\DtSearch4Studio.json";
		public static readonly string EmptyProvider = "Provider couldn't be set up, because no index was selected!";
		public static readonly string CancelProviderMessage = "When you cancel the changes, please remove the provider, because the setup is not finished and the provider will not work as expected";

		// Logging messages
		public static readonly string Browse = "Browse method";
		public static readonly string WriteToFile = "WriteToFile method";
		public static readonly string CreateTranslationProvider = "CreateTranslationProvider method";
		public static readonly string GetResults = "GetResults method. Error ";
		public static readonly string SearchSegments = "SearchSegments method.";
		public static readonly string SearchSegment = "SearchSegment method.";

		public static string DocScript = @"
<script>
var firstHitDone;
var iHit = 0;
var hitCount = %HITCOUNT%;

    function nextHit() {
        if (iHit < hitCount)
    	    gotoNthHit(iHit+1);
        }
    function prevHit() {
    	if (iHit > 1) 
	        gotoNthHit(iHit-1);
        }

    function firstHit()
    {
        if (firstHitDone != 1) {
            firstHitDone = gotoNthHit(1);
            }
    }

    function getOffset(obj)
    {
        var offset = obj.offsetTop;
        while (obj.offsetParent)
        {
            obj = obj.offsetParent;
            offset += obj.offsetTop;
        }
        return offset;
    }

    function getScrollOffset()
    {
        // Browsers other than IE 8 and earlier
        if (window.pageYOffset != null)
            return window.pageYOffset;
        // IE standards mode
        if (document.compatMode == ""CSS1Compat"")
            return document.documentElement.scrollTop;
        // IE quirks mode
        return document.body.scrollTop;
    }

    function gotoNthHit(n) { 
        iHit = n; 
        if ((n > 1) && (n == hitCount)) 
            gotoHit('hit_last'); 
        else if (n == 0) 
            gotoHit('hit0'); 
        else 
            gotoHit('hit' + n); 
     } 

    function gotoHit(where)
    {
    

        try
        {
            var a = document.getElementById(where);
            if (a == null)
            {
                return -1;
            }
            if (a.length > 1)
            {
                return -2;
            }
		    a.scrollIntoView(false);

		    if (document.body.createTextRange) {
			    // IE
            var s = document.body.createTextRange();
			    if (s != null) {
            s.moveToElementText(a);
            s.moveEnd(""word"");
				    s.select();
				    }
                }
		    else {
			    var s = document.createRange();
			    if (s != null) {
				    s.selectNodeContents(a);
				    var sel = window.getSelection();
                    sel.removeAllRanges();
				    sel.addRange(s);
			    }
            }

            return 1;
        }
        catch (ex)
        {
        }
        return -4;
    }
</script>
";
		public static string DocStyles = @"
<style>
BODY {
			font-family: segoe ui, arial;
			font-size: 10pt;
		}

.dts-field-table  
	{ 		border: 1; 
			padding: 0; 
			margin-top: 2mm; 
			margin-bottom: 2mm; 
			background-color: #F0F8FF;  
			border-radius: 10px; 
			-webkit-border-radius: 10px; 	
			-moz-border-radius: 10px; 	
			border: 0; 
	}

.dts-field-table-value-cell  
	{ 		font-size: 10pt; 
			font-family: segoe ui, arial; 
			text-align: left;  
			width: 54em;
			vertical-align: top;
			border-bottom: 1px solid #fff;
			border-top: 1px solid transparent;
	}

.dts-field-table-name-cell  
	{ 		font-size: 10pt; 
			font-family: segoe ui, arial; 
			font-weight: bold; 
			text-align: right; 
			padding-right: 1em;
			width: 8em; 
			vertical-align: top;
			border-bottom: 1px solid #fff;
			border-top: 1px solid transparent;
	}

.dts-begin-attachment
	{ 		font-size: 14pt;
			font-family: segoe ui, arial; 
			font-weight: bold; 
			margin-top: 1em;
			margin-bottom: .5em;
			background-color: #F0F8FF;  
			padding-top: .3em;
			padding-bottom: .3em;
			padding-left: 2em;
			padding-right: 2em;
			
			border-radius: 10px; 
			-webkit-border-radius: 10px; 	
			-moz-border-radius: 10px; 	
			border: 0; 
			width: 62em;
	}			

.dts-begin-file
	{ 		font-size: 14pt;
			font-family: segoe ui, arial; 
			font-weight: bold; 
			margin-top: 1em;
			margin-bottom: .5em;
			background-color: #F0F8FF;  
			padding-top: .3em;
			padding-bottom: .3em;
			padding-left: 2em;
			padding-right: 2em;
			border-radius: 10px; 
			-webkit-border-radius: 10px; 	
			-moz-border-radius: 10px; 	
			border: 0; 
			width: 62em;
	}			

.dts-begin-worksheet
	{
 		font-size: 12pt;
			font-family: segoe ui, arial; 
			font-weight: bold; 
			margin-top: 1em;
			margin-bottom: .5em;
			background-color: #F0F8FF;  
			padding-top: .3em;
			padding-bottom: .3em;
			padding-left: 2em;
			padding-right: 2em;
			border-radius: 10px; 
			-webkit-border-radius: 10px; 	
			-moz-border-radius: 10px; 	
			border: 0; 
			width: 42em;		
		
	}
	
table.dts-worksheet-table
	{
		border: 1px;
		padding-top: .3em;
		padding-bottom: .3em;
	}		

p.dts-section-break 
	{	margin: 0 0 0 0;
		padding: 0 0 0 0;
		background-color: #F0F8FF;  
		display: block;	
		height: 2px;
	}	
	
sub { vertical-align:text-bottom; font-size:75%; }
sup { vertical-align:text-top; font-size:75%; }	 	
</style>
";
	}
}