﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolderWhoDidNotGetRebate.aspx.cs" Inherits="HoldingTaxWebApp.WebForms.Tax.HolderWhoDidNotGetRebate" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>গৃহকরদাতা যারা সময়মত ট্যাক্স দেননি</h1>
        </div>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
    </form>
</body>
</html>
