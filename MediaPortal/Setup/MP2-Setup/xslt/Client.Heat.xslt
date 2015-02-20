﻿<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns="http://schemas.microsoft.com/wix/2006/wi"
                xmlns:wix="http://schemas.microsoft.com/wix/2006/wi"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:key name="MP2-Client.exe"
           match="//wix:Component[wix:File/@Source = '$(var.MediaPortal.Client.TargetDir)\MP2-Client.exe']"
           use="@Id"/>

  <!-- Copy all nodes from source to target and apply templates. -->
  <xsl:template match="node()|@*">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
    </xsl:copy>
  </xsl:template>
  
  <!-- Add comment to the beginning of the file and continue applying. -->
  <xsl:template match="wix:Wix">
    <Wix>
      <xsl:comment>This file was generated by the XSLT stylesheet 'Client.Heat.xslt'</xsl:comment>
      <xsl:apply-templates/>
    </Wix>
  </xsl:template>

  <!--
  Remove application executable from harvested file. It is explicitly defined
  in 'Features\*.wxs to reuse it for shortcut creation or service registration.
  -->
  <xsl:template match="wix:Component[@Id = key('MP2-Client.exe', @Id)/@Id]"/>
  <xsl:template match="wix:ComponentRef[@Id = key('MP2-Client.exe', @Id)/@Id]"/>

  <!-- Explicitly set DirectoryID to allow referencing to it. -->
  <xsl:template match="wix:DirectoryRef[@Id = 'INSTALLDIR_CLIENT']/wix:Directory[@Name = 'Plugins']">
    <Directory Id="CLIENT_PLUGINS_FOLDER" Name="Plugins">
      <xsl:apply-templates/>
    </Directory>
  </xsl:template>

  <!-- Rewrite definition for MP2-ClientLauncher (fixed GUID, add to autostart...) -->
  <xsl:key name="MP2-ClientLauncher.exe"
           match="//wix:Component[wix:File/@Source = '$(var.MediaPortal.Client.TargetDir)\Tools\MP2-ClientLauncher\MP2-ClientLauncher.exe']"
           use="@Id"/>

  <xsl:template match="wix:Component[@Id = key('MP2-ClientLauncher.exe', @Id)/@Id]">

    <Component Id="ClientLauncher.exe" Guid="51C55F34-A2BC-45FB-8949-7104556C7AD9">
      <File Id="ClientLauncher.exe" KeyPath="yes" Source="$(var.MediaPortal.Client.TargetDir)\Tools\MP2-ClientLauncher\MP2-ClientLauncher.exe" />
    </Component>

    <!-- Auto-start via Registry -->
    <Component Id="ClientLauncher.Registry.AutoStart" Guid="B602BA6F-90F7-4450-8E8C-C9EDFC6BEE6D">
      <RegistryValue Root="HKCU"
                     Key="Software\Microsoft\Windows\Currentversion\Run"
                     Name="MP2 ClientLauncher"
                     Value='"[#ClientLauncher.exe]"'
                     KeyPath="yes"
                     Type="expandable"/>
    </Component>

  </xsl:template>

  <xsl:template match="wix:ComponentRef[@Id = key('MP2-ClientLauncher.exe', @Id)/@Id]">
    <ComponentRef Id="ClientLauncher.exe" />
    <ComponentRef Id="ClientLauncher.Registry.AutoStart" />
  </xsl:template>
</xsl:stylesheet>
