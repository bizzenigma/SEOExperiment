<%@ Page Title="SEO Analyser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SEOExperiment._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        table, td, th {
            border: 1px solid black;
        }

    </style>

    <div class="jumbotron">
        <h3>Analyse your text?</h3>
        <h4>
            <input type="checkbox" id="CheckBox1" />
            Filter stop words 
            <input type="checkbox" id="CheckBox2" />
            Include MetaTags
            <input type="checkbox" id="CheckBox3" />
            Include external links
        </h4>

        <p></p>
        <p>
            <asp:TextBox ID="SearchText" name="SearchText" type="text" placeholder="Type your search text or web url here." runat="server" TextMode="MultiLine" Rows="5" Width="100%" Font-Size="Medium"></asp:TextBox>
        </p>
        <p><a class="btn btn-primary btn-lg" href="javascript:ProcessText()">Search</a></p>
    </div>
    <br />
    
    <div id="resultDisplay" class="jumbotron">
        <table id="wordCountTable">
            <thead>
                <tr>
                    <td><b>Word</b></td>
                    <td><b>Count</b></td>
                </tr>
            </thead>
        </table>
        <table id="metatagsTable">
            <thead>
                <tr>
                    <td><b>Metatag</b></td>
                    <td><b>Content</b></td>
                </tr>
            </thead>
        </table>
        <table id="externalLinksTable">
            <thead>
                <tr>
                    <td><b>Link</b></td>
                    <td><b>Count</b></td>
                </tr>
            </thead>
        </table>
    </div>

    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
        </div>
    </div>

    <script type="text/javascript">
        document.getElementById("resultDisplay").style.display = "none";
        document.getElementById("wordCountTable").style.display = "none";
        document.getElementById("wordCountTable").style.display = "none";
        document.getElementById("metatagsTable").style.display = "none";
        document.getElementById("externalLinksTable").style.display = "none";

        function ProcessText() {
            ClearTablesContent("wordCountTable");
            ClearTablesContent("metatagsTable");
            ClearTablesContent("externalLinksTable");

            var actionRequest = {
                FindText: document.getElementById('<%=SearchText.ClientID%>').value, IsFilterStopWords: document.getElementById("CheckBox1").checked,
                IsCountNumberofWords: true, IsMetaTagsInfo: document.getElementById("CheckBox2").checked,
                IsGetExternalLink: document.getElementById("CheckBox3").checked
            }
            var data = { request: actionRequest }
            $.ajax({
                type: 'POST',
                url: "/Services/AnalyserService.asmx/SearchAnalyser",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var analysisResult = JSON.parse(result.d);
                    if (analysisResult.Success == true) {
                        DisplayWordCountResult(analysisResult.Result.Words);
                        DisplayMetatagsResult(analysisResult.Result.MetaTags);
                        DisplayExternalLinksResult(analysisResult.Result.ExternalLinks);
                    }
                    else if (analysisResult.Success == false) {
                        alert(analysisResult.ErrorMessage);
                    }
                    else {
                        alert("No Input");
                    }
                },
                error: function (ex) {
                    alert(ex.statusText);
                }
            });
        }

        function ClearTablesContent(tableName) {
            var tableHeaderRowCount = 1;
            var table = document.getElementById(tableName);

            var rowCount = table.rows.length;
            for (var i = tableHeaderRowCount; i < rowCount; i++) {
                table.deleteRow(tableHeaderRowCount);
            }
        }

        function DisplayWordCountResult(result) {
            if (Array.isArray(result) && result.length) {
                document.getElementById("resultDisplay").style.display = "block";

                var wordCountTable = document.getElementById("wordCountTable");
                wordCountTable.style.display = "table";

                result.forEach(function (arrayItem) {
                    var row = wordCountTable.insertRow(-1);
                    var cell1 = row.insertCell(0);
                    var cell2 = row.insertCell(1);
                    cell1.innerHTML = arrayItem.Name;
                    cell2.innerHTML = arrayItem.Total;
                });
            }
        }

        function DisplayMetatagsResult(result) {
            if (Array.isArray(result) && result.length) {
                document.getElementById("resultDisplay").style.display = "block";

                var metatagsTable = document.getElementById("metatagsTable");
                metatagsTable.style.display = "table";

                result.forEach(function (arrayItem) {
                    var row = metatagsTable.insertRow(-1);
                    var cell1 = row.insertCell(0);
                    var cell2 = row.insertCell(1);
                    cell1.innerHTML = arrayItem.Name;
                    cell2.innerHTML = arrayItem.Content;

                    DisplayWordCountResult(arrayItem.WordsInfoList);
                    DisplayExternalLinksResult(arrayItem.ExternalLinks);
                });
            }
        }

        function DisplayExternalLinksResult(result) {
            if (Array.isArray(result) && result.length) {
                document.getElementById("resultDisplay").style.display = "block";

                var externalLinksTable = document.getElementById("externalLinksTable");
                externalLinksTable.style.display = "table";

                result.forEach(function (arrayItem) {
                    var row = externalLinksTable.insertRow(-1);
                        row.style.display = "table-row";
                        var cell1 = row.insertCell(0);
                        var cell2 = row.insertCell(1);
                    cell1.innerHTML = arrayItem.Name;
                    cell2.innerHTML = arrayItem.Total;
                });
            }
        }

    </script>

</asp:Content>
