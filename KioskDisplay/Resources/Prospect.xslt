<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes"/>
  <xsl:template match="/">
    <table>
      <tr>
        <xsl:for-each select="QuoteWizardPageViewModel/*">
          <th>
            <xsl:value-of select ="local-name()"/>
          </th>
        </xsl:for-each>
      </tr>
      <xsl:for-each select="QuoteWizardPageViewModel">
        <tr>
          <xsl:for-each select="*">
            <td>
              <xsl:value-of select="."/>
            </td>
          </xsl:for-each>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>