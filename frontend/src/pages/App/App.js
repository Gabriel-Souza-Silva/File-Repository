import React, {useEffect, useState} from 'react';
import './App.css';

import api from '../../services/api'

function App() {

  const [isEditting,setIsEditting] = useState(false)
  const [files, setFiles] = useState([])
  const [titulo, setTitulo] = useState('')
  const [ator, setAtor] = useState('')
  const [diretor, setDiretor] = useState('')
  const [midia, setMidia] = useState(-1)
  const [anoInicio, setAnoInicio] = useState(1950)
  const [anoFim, setAnoFim] = useState(2100)
  const [file, setFile] = useState(null)
  const [idFile, setIdFile] = useState(null)

  useEffect(() => {

    async function fetchData(){
      
      const response = await api.get('/files')

      setFiles(response.data)
    }

    fetchData()
    
  },[])

  function getMidiaType(intMediaType){
    switch(intMediaType){
      case 0: return "FOTO"
      case 1: return "VIDEO"
      case 2: return "DVD"
      case 3: return "BLUERAY"
      default: return "PDF"
    }
  }

  function tratarDataInicio(ano){

    if(ano < 1950){
      setAnoInicio(1950)
    }else{
      setAnoInicio(ano)
    }
    
  }

  function tratarDataFim(ano){
    if(ano > 2100){
      setAnoFim(2100)
    }else{
      setAnoFim(ano)
    }
  }

  function handleEdit(e,file){
    e.preventDefault();

    setIdFile(file.id)
    setTitulo(file.titulo)
    setAtor(file.elenco)
    setDiretor(file.direcao)
    setMidia(file.tipodeMidia.toString())
    setIsEditting(true);
  }

  async function handleDelete(e, id){
    e.preventDefault();

    await api.delete(`/files/${id}`)
  }

  function VerifyFields(){
    if(file === null || titulo === '' || ator === '' || diretor === ''|| midia === -1 ||anoInicio ===''){
      return false
    }

    return true
  }

  async function handleSearch(e){
    e.preventDefault()

    const response = await api.get('./files/search',{
      params:{
        titulo,
        diretor,
        elenco:ator,
        anoInicio,
        anoFim
      }
    })

    setFiles(response.data)
  }

  async function handleEditOrNew(e){
    e.preventDefault();

    if(isEditting){

      console.log(`/files/update/${idFile}`)
      //USANDO POST POIS NAO ATUALIZO O ARQUIVO

      const data = new FormData();

      data.append('Titulo',titulo)
      data.append('Ano',anoInicio)
      data.append('TipodeMidia',midia)
      data.append('Direcao',diretor)
      data.append('Elenco',ator)

      const response = await api.post(`/files/update/${idFile}`,data)

      console.log(response)
    }else{

      const data = new FormData();

      
      if(VerifyFields()){
        //PROPRIEDADES FIXAS ATE INCLUSAO DOS CAMPOS
        data.append('Titulo',titulo)
        data.append('Sinopse',titulo)
        data.append('Duracao','2h20min')
        data.append('Ano',anoInicio)
        data.append('TipodeMidia',midia)
        data.append('Direcao',diretor)
        data.append('Elenco',ator)
        data.append('files',file)

        await api.post('/files', data)   
      }else{
        alert('Preenche Tudo Irmão')
      }
    }
    
  }

  return (
    <div className="container">
      <form onSubmit={()=>{}}>
        <div className="inputContainer">
          <div>Titulo:</div> 
          <input 
            className="inputText" 
            type="text"
            value={titulo || ''}
            onChange={e=> setTitulo(e.target.value)}/>
        </div>

        <div className="inputContainer"> 
          <div>Ator/Atriz:</div>
          <input 
            className="inputText" 
            type="text" 
            value={ator || ''}
            onChange={e=>setAtor(e.target.value)}/>
        </div>

        <div className="inputContainer"> 
          <div>Diretor:</div>
          <input 
            className="inputText" 
            type="text" 
            value={diretor || ''}
            onChange={e=>setDiretor(e.target.value)}/>
        </div>

        <div className="inputContainer"> 
          <div>Midia:</div>
          <select className="itemSelect" value={midia} onChange={e => setMidia(e.target.value)}>
            <option value="-1" defaultValue>TODOS</option>
            <option value="0">FOTO</option>
            <option value="1">VIDEO</option>
            <option value="2">DVD</option>
            <option value="3">BLUERAY</option>
            <option value="4">PDF</option>
            <option value="5">RAR</option>
            <option value="6">ZIP</option>
          </select>
        </div>

        <div className=" inputContainer inputContainerYear">
          <div>
              <span>Inicio</span>
              <input 
                className="inputYear" 
                type="number"
                value={anoInicio}
                onChange={e=> tratarDataInicio(e.target.value)} />
          </div>
          
          <div>
              <span>Fim</span>
              <input 
              className="inputYear" 
              type="text" 
              value={anoFim || ''}
              onChange={e=> tratarDataFim(e.target.value)}/>
          </div>
        </div> 

        <div className=" inputContainer">
          <input 
            type="file" 
            style={{display: !isEditting ? 'block' : 'none' }}
            onChange={e => {console.log(e.target.files[0]); setFile(e.target.files[0])}}/>
        </div>
        {//CAMPOS PARA INCLUSAO DE NOVO ARQUIVO 
        /*
        <input 
              className="inputText" 
              type="text" 
              value={sinopse || ''}
              onChange={e=>setSinopse(e.target.value)}/>
        
        <div className=" inputContainer">
          <input type="file" visible={!isEditting}/>
        </div>
        */
        }
        <div className="btnMenuContainer">
          <input onClick={(e)=>{handleSearch(e)}} className="btnMenu" type="submit" value="Consultar"/>
          <input onClick={(e)=>{handleEditOrNew(e)}} className="btnMenu" type="submit" value={isEditting ? "Salvar" : "Novo"}/>
        </div>        
      </form>

      <table className="content-table">
      <thead>
        <tr>
          <th>Titulo</th>
          <th>Ano</th>
          <th>Duração</th>
          <th>Midia</th>
          <th>Opções</th>
        </tr>
      </thead>
      <tbody>

        {files.map(file =>(
          <tr key={file.id}>
            <td>{file.titulo}</td>
            <td>{file.ano}</td>
            <td>{file.duracao}</td>
            <td>{getMidiaType(file.tipodeMidia)}</td>
            <td className="buttonCell">
              <button className="btn" onClick={(e)=>{handleEdit(e, file)}}>Editar</button>
              <button className="btn" onClick={(e)=>{handleDelete(e, file.id)}}>Apagar</button>
            </td>
        </tr>
        ))}

      </tbody>
      </table>
      
    </div>
  )
}

export default App;
