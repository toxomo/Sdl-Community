<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <html>
			<style>
				.title {
				  font-family: Verdana;
				  color: #6E7E82;
				  font-size: 130%;
					padding-bottom:20px;
				}
				.filesTile {
				  font-family: Verdana;
				  color: #6E7E82;
				  font-size: 100%;
				}
				.proj {
					position: relative;
				  width: auto;
				  height: auto;
				}
				.projectInfo {
				   overflow-x:auto;
				   color: #999999;
				   position: relative;
				   bottom: 20px;
           left: 300px;
				}				
				.table {
				 position: relative;
				 top:350px;
				 padding-bottom:20px;
				}
				
				#files {
				  font-family: Verdana, Helvetica, sans-serif;
				  border-collapse: collapse;
				  width: 100%;				 
				}
			  #files td {
				  border: 1px solid #ddd;
				  padding: 8px;
				  font-size: 80%;
				}
				#files tr:nth-child(even){background-color: #f2f2f2;}
				#files tr:hover {background-color: #ddd;}
				
				#files th {
				  padding-top: 12px;
				  padding-bottom: 12px;
				  text-align: left;
				  background-color: #476878;
				  color: white;
					font-size: 85%;
				}
			
			</style>
			<head>
				<p class="title">Signoff Verify Settings</p>
			</head>
      <body>
				<div class="proj">
					<b><xsl:text>Project: </xsl:text></b>
					<div class="projectInfo"><xsl:value-of select="//ProjectInformation/Project/@Name"/></div>
						
					<b><xsl:text>Source Language: </xsl:text>	</b> 
					<div class="projectInfo"><xsl:value-of select="//ProjectInformation/SourceLanguage/@DisplayName"/></div>	
						
					<xsl:for-each select="//ProjectInformation/TargetLanguages/TargetLanguage">
						<b><xsl:text>Target Language:  </xsl:text></b>
						<div class="projectInfo"> <xsl:value-of select="DisplayName"/></div>				
					</xsl:for-each>				
						
					<b><xsl:text>Run at (project level): </xsl:text></b>
					<div class="projectInfo"><xsl:value-of select="//ProjectInformation/RunAt"/></div>	
						
					<xsl:for-each select="//ProjectInformation/TranslationMemories/TranslationMemory">
						<b><xsl:text>Translation Memory: </xsl:text></b>
						<div class="projectInfo"><xsl:value-of select="Name"/></div>					
					</xsl:for-each>
						
					<xsl:for-each select="//ProjectInformation/Termbases/Termbase">
						<b><xsl:text>Termbase: </xsl:text></b>
						<div class="projectInfo"><xsl:value-of select="Name"/></div>				
					</xsl:for-each>
						
					<b><xsl:text>RegEx rules: </xsl:text></b>
					<div class="projectInfo"><xsl:value-of select="//ProjectInformation/RegExRules"/></div>				
				
						<b><xsl:text>Check RegEx: </xsl:text></b> 
					<div class="projectInfo"><xsl:value-of select="//ProjectInformation/CheckRegEx"/></div>		
				
						<b><xsl:text>QA Checker: </xsl:text></b>
					<div class="projectInfo"><xsl:value-of select="//ProjectInformation/QAChecker"/></div>
					<br></br><br></br><br></br><br></br>
			</div>
				
			<div class="table">
			 <b class="filesTile"><xsl:text>Language Files </xsl:text></b>
			 <br></br>
			 <br></br>				
				<div style="overflow-x:auto;">
					<table id="files">
						<tr>
							<th>Name</th>
							<th>Target language</th>
							<th>Run at</th>
							<th>Phase</th>
							<th>Is current assigned</th>
							<th>Assignees number</th>
							<th>Number Verifier</th>
						</tr>
						<xsl:for-each select="//ProjectInformation/LanguageFiles/LanguageFile">
							<tr>
								<td>
									<xsl:value-of select="@Name"/>
								</td>
								<td>
									<xsl:value-of select="@TargetLanguage"/>
								</td>
								<td>
									<xsl:value-of select="@RunAt"/>
								</td>
								<td>
									<xsl:value-of select="Phase/@AssignedPhase"/>
								</td>
								<td>
									<xsl:value-of select="Phase/@IsCurrentAssignment"/>
								</td>
								<td>
									<xsl:value-of select="Phase/@AssigneesNumber"/>
								</td>
								<td>
									<xsl:value-of select="NumberVerifier/@ExecutedDate"/>
								</td>
							</tr>
						</xsl:for-each>
					</table>
				</div>
			</div>
			</body>
    </html>
  </xsl:template>
</xsl:stylesheet>