# API-DataGovernance
Provides business definition of matching terms in API response from Factset business terms glossary.

The feature can be used with APIs developed in .net core. 

<b>Steps For API developers :</b>
<ol>
  <li>Add <NUGET_REPO_HERE> as package source in nuget package manager</li>
  <li>install FS_Glossary from above added package source</li>
  <li>In Configure method in Startup file <b>add the middleware</b> using the line - <pre>applicationbuilder.UseGlossary()</pre></li>
  <li>To add option to selectively add header to enable glossary <b>add following operation filter</b> while adding swaggergen in ConfigureServices <pre>swaggerGen.OperationFilter&lt;AddGlossaryHeaderFilter&gt;();</pre></li>
</ol>

<b>Steps For API consumers :</b>
<ol>
  <li>Send request header "BusinessTerms" with value "Include" to get business definitions with API response</i>
  <li>For APIs running from swagger the header parameter can be set to Include (Given the developer has enabled operation filter provided in Nuget as told in step 4 above)</li>
</ol>
