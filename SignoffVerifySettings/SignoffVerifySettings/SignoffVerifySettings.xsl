<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <html>
			<style>
				.Title{
				font-family:verdana;
				color: #6E7E82;
				font-size:130%;
				}
			</style>
			<head>
				<p class="Title">Signoff Verify Settings</p>
			</head>
      <body>				
				<p>
					<b><xsl:text>Project: </xsl:text></b>
					<xsl:value-of select="//ProjectInformation/Project/@Name"/>
				</p>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>