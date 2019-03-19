<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
			<html>
				<style>
				</style>
				
				<body>
							<p>Project: <xsl:value-of select="//ProjectInformation/@Project" /></p>						
			
				</body>
			 </html>
    </xsl:template>
</xsl:stylesheet>