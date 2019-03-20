<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <html>
			<style>
				.h1{
				font-family: Verdana, sans-serif;
				color: #5D6E7F;
				}
			</style>
			<head>
				<h1>Signoff Verify Settings</h1>
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