<html>
	<head>
		<title>Código Fonte - Teste de segurança de senha</title>
		<script language="javascript"> 
/*
código adaptado por Andreia_Sp
Original: www.brunogross.com
*/
function Security(val, tamanho) {
 	
	document.getElementById('mensagem').innerHTML = "";
 	document.getElementById('d_baixa').style.background = 'white';
 	document.getElementById('d_media').style.background = 'white';
 	document.getElementById('d_alta').style.background = 'white';
 
	if( val.length >= tamanho && val.search(/[a-z]/) != -1 && val.search(/[A-Z]/) != -1 && val.search(/[0-9]/) != -1 
		||val.length >= tamanho && val.search(/[a-z]/) != -1 && val.search(/[A-Z]/) != -1 && val.search(/[@!#$%&*+=?|-]/) 
		||val.length >= tamanho && val.search(/[a-z]/) != -1 && val.search(/[@!#$%&*+=?|-]/) != -1 && val.search(/[0-9]/) 
		||val.length >= tamanho  && val.search(/[@!#$%&*+=?|-]/) != -1 && val.search(/[A-Z]/) != -1 && val.search(/[0-9]/) )
		{
				document.getElementById('mensagem').innerHTML = "forte";
  			document.getElementById('d_baixa').style.background = 'green';
 				document.getElementById('d_media').style.background = 'green';
 				document.getElementById('d_alta').style.background = 'green';
  		
  	} 
  	else{
  		if( val.length >= tamanho && val.search(/[a-z]/) != -1 && val.search(/[A-Z]/) != -1 
  			||val.length >= tamanho && val.search(/[a-z]/) != -1 && val.search(/[0-9]/) != -1 
  			||val.length >= tamanho && val.search(/[a-z]/) != -1 && val.search(/[@!#$%&*+=?|-]/) != -1
				||val.length >= tamanho && val.search(/[A-Z]/) != -1 && val.search(/[0-9]/) != -1
				||val.length >= tamanho && val.search(/[A-Z]/) != -1 && val.search(/[@!#$%&*+=?|-]/) != -1
				||val.length >= tamanho && val.search(/[0-9]/) != -1 && val.search(/[@!#$%&*+=?|-]/) != -1){

				document.getElementById('mensagem').innerHTML = "média";
 				document.getElementById('d_baixa').style.background = 'yellow';
 				document.getElementById('d_media').style.background = 'yellow';
 				document.getElementById('d_alta').style.background = 'white';
  				
  			} 
  			else {
  				if(val.length >= tamanho)
  				{
	  				document.getElementById('mensagem').innerHTML = "fraca";
  					document.getElementById('d_baixa').style.background = 'red';
 						document.getElementById('d_media').style.background = 'white';
 						document.getElementById('d_alta').style.background = 'white';
  					
  				}
  		}
  	}
  }
  </script>
	</head>
	<body>
		<form>
			<input type="password" id="pass" onkeyup="Security(this.value, 5);">
		</form>
		<FONT face="Arial" size="2"><STRONG>Nível de Segurança:</STRONG></FONT>
		<table cellpadding="0" cellspacing="0" style="BORDER-RIGHT:#000 1px solid; BORDER-TOP:#000 1px solid; BORDER-LEFT:#000 1px solid; BORDER-BOTTOM:#000 1px solid">
			<tr>
				<td width="50" align="center">
					<div id="d_baixa">&nbsp;
					</div>
				</td>
				<td width="50" align="center">
					<div id="d_media">&nbsp;
					</div>
				</td>
				<td width="50" align="center">
					<div id="d_alta">&nbsp;
					</div>
				</td>
			</tr>
		</table>
		<br><div id="mensagem"></div>
	</body>
</html>
