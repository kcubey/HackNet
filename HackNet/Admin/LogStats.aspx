<%@ Page Title="Log Statistics" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="LogStats.aspx.cs" Inherits="HackNet.Admin.LogStats" %>

<asp:Content ContentPlaceHolderID="AdminHeadContent" runat="server">
	<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
	<script type="text/javascript">
	  google.charts.load('current', {packages: ['line', 'bar']});
	  google.charts.setOnLoadCallback(drawChart);

	  function drawChart() {
		var data1 = new google.visualization.DataTable();
		data1.addColumn('string', 'Date');
		data1.addColumn('number', 'INFO');
		data1.addColumn('number', 'WARN');
		data1.addColumn('number', 'ERROR');
	  	data1.addRows(<%= jsArrayS7D() %>);

		var data2 = new google.visualization.DataTable();
		data2.addColumn('string', 'Date');
		data2.addColumn('number', 'Game');
		data2.addColumn('number', 'Security');
		data2.addColumn('number', 'Profile');
		data2.addColumn('number', 'Payment');
	  	data2.addRows(<%= jsArrayT7D() %>);

		var data3 = new google.visualization.DataTable();
		data3.addColumn('string', 'Hour');
		data3.addColumn('number', 'INFO');
		data3.addColumn('number', 'WARN');
		data3.addColumn('number', 'ERROR');
	  	data3.addRows(<%= jsArrayS24H() %>);

	   var options1 = {
		  chart: {
			title: 'HackNet Event Count - Past 7 days by Severity',
			subtitle: 'Logged by HackNet Audit Logs',
		  },
		  height: 500,
		  axes: {
			x: { 0: {label: 'Date of Occurence'} },
			y: { 0: {label: 'Events'} }
		  }
	   };

	   var options2 = {
		  chart: {
			title: 'HackNet Event Count - Past 7 days by Log Type',
			subtitle: 'Logged by HackNet Audit Logs',
		  },
		  height: 500,
		  axes: {
			x: { 0: {label: 'Date of Occurence'} },
			y: { 0: {label: 'Events'} }
		  }
	   };

	   var options3 = {
		  chart: {
			title: 'HackNet Event Count - Past 12 hours by Severity',
			subtitle: 'Logged by HackNet Audit Logs',
		  },
		  height: 500,
		  axes: {
			x: { 0: {label: 'Date of Occurence'} },
			y: { 0: {label: 'Events'} }
		  }
	   };

	   var options4 = {
		  chart: {
			title: 'HackNet Event Count - Past 7 days by Severity',
			subtitle: 'Logged by HackNet Audit Logs',
		  },
		  height: 500,
		  bars: 'horizontal',
		  axes: {
			x: { 0: {label: 'Date of Occurence'} },
			y: { 0: {label: 'Events'} }
		  }
	   };

	  	var chart1 = new google.charts.Line(document.getElementById('chart1'));
	  	var chart2 = new google.charts.Line(document.getElementById('chart2'));
	  	var chart3 = new google.charts.Line(document.getElementById('chart3'));
		var chart4 = new google.charts.Bar(document.getElementById('chart4'));

	  	chart1.draw(data1,  options1);
	  	chart2.draw(data2,  options2);
	  	chart3.draw(data3,  options3);
	  	chart4.draw(data1,  options4);
	  }

	</script>
	<style>
		.chart {
			background-color:white;
			padding:20px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
	<div id="chart1" class="chart"></div>
	<div id="chart4" class="chart"></div>
	<div id="chart2" class="chart"></div>
	<div id="chart3" class="chart"></div>
</asp:Content>
