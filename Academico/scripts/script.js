var tbody = document.querySelector('table tbody');
var aluno = {};

function Cadastrar() 
{	
	aluno.nome = document.querySelector('#nome').value;
	aluno.sobreNome = document.querySelector('#sobreNome').value;
	aluno.telefone = document.querySelector('#telefone').value;
	aluno.ra = document.querySelector('#ra').value;

	console.log(aluno);

	if ( aluno.id === undefined || aluno.id === 0 ) 
	{
		salvarEstudantes('POST', 0, aluno);
	}
	else
	{
		salvarEstudantes('PUT', aluno.id, aluno);
	}

	carregaEstudantes();

	$('#myModal').modal('hide');
}

function NovoAluno()
{
	var btnSalvar = document.querySelector('#btnSalvar');
			// var btnCancelar = document.querySelector('#btnCancelar');
			var tituloModal = document.querySelector('#tituloModal');

			document.querySelector('#nome').value = '';
			document.querySelector('#sobreNome').value = '';
			document.querySelector('#telefone').value = '';
			document.querySelector('#ra').value = '';

			aluno = {};
			
			btnSalvar.textContent = 'Cadastrar';
			//btnCancelar.textContent = 'Limpar';	

			tituloModal.textContent = 'Cadastrar Aluno';

			$('#myModal').modal('show');
		}

		function Cancelar()
		{
			var btnSalvar = document.querySelector('#btnSalvar');
			// var btnCancelar = document.querySelector('#btnCancelar');
			var tituloModal = document.querySelector('#tituloModal');

			document.querySelector('#nome').value = '';
			document.querySelector('#sobreNome').value = '';
			document.querySelector('#telefone').value = '';
			document.querySelector('#ra').value = '';

			aluno = {};
			
			btnSalvar.textContent = 'Cadastrar';
			//btnCancelar.textContent = 'Limpar';	

			tituloModal.textContent = 'Cadastrar Aluno';

			$('#myModal').modal('hide');
		}

		function carregaEstudantes() 
		{
			tbody.innerHTML = '';

			var xhr = new XMLHttpRequest();
			
			xhr.open('GET', `http://localhost:55973/Api/Aluno`, true);

			xhr.onload = function () {
				var estudantes = JSON.parse(this.responseText);
				for (var indice in estudantes ) {
					adicionaLinha(estudantes[indice]);
				}
			}
			
			xhr.send();
		}

		function salvarEstudantes(metodo, id, corpo) 
		{
			var xhr = new XMLHttpRequest();
			
			if (id === undefined || id === 0) 
				id = '';

			xhr.open(metodo, `http://localhost:55973/Api/Aluno/${id}`, false);

			xhr.setRequestHeader('content-type', 'application/json');
			xhr.send(JSON.stringify(corpo));
		}

		function excluirEstudante(id)
		{
			var xhr = new XMLHttpRequest();

			xhr.open('Delete', `http://localhost:55973/Api/Aluno/${id}`, false);

			xhr.send();
		}
		
		function excluir(estudante)
		{
			bootbox.confirm({
				message: `Confirma exclusão do ${estudante.nome}?`,
				buttons: {
					confirm: {
						label: 'SIM',
						className: 'btn-success'
					},
					cancel: {
						label: 'NÃO',
						className: 'btn-danger'
					}
				},
				callback: function (result) {
					if (result)
					{
						excluirEstudante(estudante.id);
						carregaEstudantes();
					}
				}
			});
			
		}

		carregaEstudantes();

		function editarEstudante(estudante)
		{
			var btnSalvar = document.querySelector('#btnSalvar');
			//var btnCancelar = document.querySelector('#btnCancelar');
			var tituloModal = document.querySelector('#tituloModal');

			document.querySelector('#nome').value = estudante.nome;
			document.querySelector('#sobreNome').value = estudante.sobreNome;
			document.querySelector('#telefone').value = estudante.telefone;
			document.querySelector('#ra').value = estudante.ra;


			btnSalvar.textContent = 'Salvar';
			//btnCancelar.textContent = 'Cancelar';	

			tituloModal.textContent = `Editar Aluno ${estudante.nome}`;

			aluno = estudante;

			console.log(aluno);

		}

		function adicionaLinha(estudante) {
			

			var trow = `<tr>
			<td>${estudante.nome}</td>
			<td>${estudante.sobreNome}</td>
			<td>${estudante.telefone}</td>
			<td>${estudante.ra}</td>
			<td>
			<button class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick='editarEstudante(${JSON.stringify(estudante)})'>Editar</button>
			<button class="btn btn-danger" onclick='excluir(${JSON.stringify(estudante)})'>Excluir</button>
			</td>
			</tr>
			`
			tbody.innerHTML += trow;
		}