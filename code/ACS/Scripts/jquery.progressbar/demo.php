<?php
	header("Cache-Control: no-cache, must-revalidate");
	header("Expires: Mon, 26 Jul 1997 05:00:00 GMT");

	if (@$_GET['id']) {
		echo json_encode(uploadprogress_get_info($_GET['id']));
		exit();
	}
	
	$uuid = uniqid();
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>jQuery Progress Bar v1.1</title>
		<script type="text/javascript" src="js/jquery.js"></script>
		<script type="text/javascript" src="js/jquery.progressbar.min.js"></script>
		<script>
			var progress_key = '<?= $uuid ?>';
			
			$(document).ready(function() {
				$("#spaceused1").progressBar();
				$("#spaceused2").progressBar({ barImage: 'images/progressbg_yellow.gif'} );
				$("#spaceused3").progressBar({ barImage: 'images/progressbg_orange.gif', showText: false} );
				$("#spaceused4").progressBar(65, { showText: false, barImage: 'images/progressbg_red.gif'} );
				$("#uploadprogressbar").progressBar();
			});
			
			function showUpload() {
				$.get("demo.php?id=" + progress_key, function(data) {
					if (!data)
						return;

					var response;
					eval ("response = " + data);

					if (!response)
						return;

					var percentage = Math.floor(100 * parseInt(response['bytes_uploaded']) / parseInt(response['bytes_total']));
					$("#uploadprogressbar").progressBar(percentage);

				});
				setTimeout("showUpload()", 750);

			}
		</script>
		<style>
			* { padding: 0px; margin: 0px; }
			body, html { font-family: Helvetica, Arial, Tahoma; font-size: 12px; line-height: 20px; font-color: #444; } 
			a { text-decoration: none; color: #3366cc; }
			table { border-collapse: collapse; border: 0px; }
			table tr { vertical-align: top; }
			table td { padding: 3px; }
			h2 { font-size: 16px; }
			pre { border: 1px dashed #ddd; color: #444; background-color: #eee; width: 100%; }
			

			#container { width: 80%; margin-left: 10%; margin-top: 30px;}
			div.contentblock { padding-bottom: 25px; }	
		</style>
	</head>
	<body>
		<div id="container">
			<div style="float: right; text-align: right; width: 300px;">download me at <a href="http://t.wits.sg">http://t.wits.sg</a></div>
			<div class="contentblock">
				<h2>Progress Bars &amp; Controls</h2>
				<table>
					<tr><td>Green Bar</td><td><span class="progressBar" id="spaceused1">25%</span></td></tr>
					<tr><td>Yellow Bar</td><td><span class="progressBar" id="spaceused2">35%</span></td></tr>
					<tr><td>Orange Bar (No Text)</td><td><span class="progressBar" id="spaceused3">55%</span></td></tr>
					<tr><td>Red Bar (No Text)</td><td><span class="progressBar" id="spaceused4">85%</span></td></tr>
					<tr><td>File Upload</td><td>
						<form action="" method="POST" id="uploadform" enctype="multipart/form-data" onsubmit="setTimeout('showUpload()', 1500);">
							<input type="hidden" name="UPLOAD_IDENTIFIER" id="progress_key" value="<?= $uuid ?>" />
							<input type="file" name="ulfile" id="ulfile" />
							<input type="submit" value="Upload" />
							<br />
							<span class="progressbar" id="uploadprogressbar">0%</span>	
						</form>
					</td></tr>
				</table>
				<strong>Some controls: </strong>
				<a href="#" onclick="$('#spaceused1').progressBar(20);">20</a> |
				<a href="#" onclick="$('#spaceused1').progressBar(40);">40</a> |
				<a href="#" onclick="$('#spaceused1').progressBar(60);">60</a> |
				<a href="#" onclick="$('#spaceused1').progressBar(80);">80</a> |
				<a href="#" onclick="$('#spaceused1').progressBar(100);">100</a>
			</div>
			
			<div class="contentblock">
				<h2>Usage: </h2>
				<pre>
	$(document).ready(function() {
		$("#spaceused1").progressBar();
		$("#spaceused2").progressBar({ barImage: 'images/progressbg_yellow.gif'} );
		$("#spaceused3").progressBar({ barImage: 'images/progressbg_orange.gif', showText: false} );
		$("#spaceused4").progressBar(65, { showText: false, barImage: 'images/progressbg_red.gif'} );
		$("#uploadprogressbar").progressBar();
	});</pre>
			</div>
		</div>
	</body>
</html>
