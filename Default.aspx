<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TODOProject._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>#container{
  width: 300px;
  display: block;
  margin: auto;
}

p {
  float: right;
}

#list{
  list-style: none;
}

#list li{
  margin: 10px;
  cursor: pointer;
}</style>

  <div id="main">
      <noscript>This site just doesn't work, period, without JavaScript</noscript>
      <h1 id="isRight"></h1>
      <ul id="list" class="ui-sortable">

        <li color="1" class="colorBlue draggertab" rel="1" id="2" >
          <span id="2listitem" title="Double-click to edit..." style="opacity: 1;">Work
          List</span>

          <div class="draggertab tab"></div>

          <div class="colortab tab"></div>

          <div class="deletetab tab" style="width: 44px; display: block; right: -64px;">
          </div>

          <div class="donetab tab"></div>
        </li>

        <li color="4" class="colorGreen draggertab" rel="2" id="4">
          <span id="4listitem" title="Double-click to edit..." style=
          "opacity: 0.5;">Saibaan List<img src="/images/crossout.png" class="crossout"
          style="width: 100%; display: block;" /></span>

          <div class="draggertab tab"></div>

          <div class="colortab tab"></div>

          <div class="deletetab tab"></div>

          <div class="donetab tab"></div>
        </li>

        <li color="1" class="colorBlue draggertab" rel="3" id="6">
          <span id="6listitem" title="Double-click to edit...">adfas</span>

          <div class="draggertab tab"></div>

          <div class="colortab tab"></div>

          <div class="deletetab tab"></div>

          <div class="donetab tab"></div>
        </li>

        <li color="1" class="colorBlue" rel="4" id="7">
          <span id="7listitem" title="Double-click to edit...">adfa</span>

          <div class="draggertab tab"></div>

          <div class="colortab tab"></div>

          <div class="deletetab tab"></div>

          <div class="donetab tab"></div>
        </li>

        <li color="1" class="colorBlue" rel="5" id="8">
          <span id="8listitem" title="Double-click to edit...">asdfas</span>

          <div class="draggertab tab"></div>

          <div class="colortab tab"></div>

          <div class="deletetab tab"></div>

          <div class="donetab tab"></div>
        </li>

        <li color="1" class="colorBlue" rel="6" id="9">
          <span id="9listitem" title="Double-click to edit...">fasdfasdf</span>

          <div class="draggertab tab"></div>

          <div class="colortab tab"></div>

          <div class="deletetab tab"></div>

          <div class="donetab tab"></div>
        </li>

        <li color="3" class="colorRed" rel="7" id="10">
          <span id="10listitem" title="Double-click to edit...">asdasfaf</span>

          <div class="draggertab tab"></div>

          <div class="colortab tab"></div>

          <div class="deletetab tab"></div>

          <div class="donetab tab"></div>
        </li>
      </ul>
	  <br />

      
        <input type="text" id="new-list-item-text" name="new-list-item-text" />
        <input type="submit" id="add-new-submit" value="Add" class="button" />
     

      <div class="clear"></div>
    </div>

   <script>
       var list = document.getElementById('list')
       var base, randomized, dragging, draggedOver;
       var isRight = 'Not In Order!';

       const genRandom = (array) => {
           base = array.slice()
           randomized = array.sort(() => Math.random() - 0.5)
           if (randomized.join("") !== base.join("")) {
               renderItems(randomized)
           } else {
               //recursion to account if the randomization returns the original array
               genRandom()
           }
       }

       const renderItems = (data) => {
           document.getElementById('isRight').innerText = isRight
           list.innerText = ''
           data.forEach(item => {
               var node = document.createElement("li");
               node.draggable = true
               node.style.backgroundColor = item
               node.style.backgroundColor = node.style.backgroundColor.length > 0
                   ? item : 'lightblue'
               node.addEventListener('drag', setDragging)
               node.addEventListener('dragover', setDraggedOver)
               node.addEventListener('drop', compare)
               node.innerText = item
               list.appendChild(node)
           })
       }

       const compare = (e) => {
           var index1 = randomized.indexOf(dragging);
           var index2 = randomized.indexOf(draggedOver);
           randomized.splice(index1, 1)
           randomized.splice(index2, 0, dragging)
           
               
           isRight='dragging from: ' + index1 + 'dragout ' +index2)=;

           renderItems(randomized)
       };


       const setDraggedOver = (e) => {
           e.preventDefault();
           draggedOver = Number.isNaN(parseInt(e.target.innerText)) ? e.target.innerText : parseInt(e.target.innerText)
           
       }

       const setDragging = (e) => {
           dragging = Number.isNaN(parseInt(e.target.innerText)) ? e.target.innerText : parseInt(e.target.innerText)
       }

       // genRandom([0, 1, 2, 3, 4, 5, 6])
       genRandom(['red', 'orange', 'yellow', 'green', 'blue', 'indigo', 'violet'])
   </script>
</asp:Content>





