#pragma checksum "D:\Users\MIKOLKA\PV911\WebCore_5.0\Views\Ajax\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ab06b8cd60dafda14d83f95fc8c2ce3954173d60"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Ajax_Index), @"mvc.1.0.view", @"/Views/Ajax/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Users\MIKOLKA\PV911\WebCore_5.0\Views\_ViewImports.cshtml"
using WebCore_5._0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Users\MIKOLKA\PV911\WebCore_5.0\Views\_ViewImports.cshtml"
using WebCore_5._0.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab06b8cd60dafda14d83f95fc8c2ce3954173d60", @"/Views/Ajax/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02bd8a2945abe5ec302fce3c79b04fefeb312879", @"/Views/_ViewImports.cshtml")]
    public class Views_Ajax_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Users\MIKOLKA\PV911\WebCore_5.0\Views\Ajax\Index.cshtml"
  
    ViewData["Title"] = "Ajax Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">
    <h1 class=""display-4"">Welcome AJAX</h1>

</div>

<section id=""mainTeachers"">
    Load...
</section>

<section id=""newTeacher"">
    <input type=""text"" id=""newTeacherName"" />
    <input type=""button"" id=""btnCreate"" value=""Create"" />
</section>


<script>

    function serverResponseAddTeacher(res) {
        console.log(res);
        fetchGetTeachers();
    }

    document.getElementById(""btnCreate"").onclick = function () {
        let newTeacher = {
            id: 0,
            name: document.getElementById(""newTeacherName"").value
        };

        fetch(""/api/Teachers"", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: JSON.stringify(newTeacher)
        })
            .then(response => response.json()) // Преобразуем ответ в json
            .then(json => serverResponseAddTeacher(js");
            WriteLiteral(@"on))
            .catch((ex) => { // обрабатываем возможную ошибку
                console.log(""Error: "" + ex.message);
                console.log(""Response: "" + ex.response);
            });
        document.getElementById(""newTeacherName"").value = """";

    }

    // Построение страницы по результатам ответа от сервера
    function renderTeachersList(json) {
        console.log(""Get From Server: "");
        console.log(json);

        //Очищаем main
        let mainTeachers = document.getElementById(""mainTeachers"");
        mainTeachers.innerHTML = """";

        //Создаем ul
        let ul = document.createElement(""ul"")

        //Создаем коллекцию элементов li и внедрю ее в ul
        for (var i = 0; i < json.length; i++) {
            let li = document.createElement(""li"");
            li.innerText = json[i].name;
            li.className = json[i].id;
            li.ondblclick = function () {
                let id = this.className;
                let name = this.innerText;
  ");
            WriteLiteral(@"              this.innerHTML = """";

                // поле id
                let inId = document.createElement(""input"");
                inId.type = ""hidden"";
                inId.id = ""editNowId"";
                inId.value = id;
                this.appendChild(inId);

                // поле имя
                let inName = document.createElement(""input"");
                inName.type = ""text"";
                inName.id = ""editNowName"";
                inName.value = name;
                this.appendChild(inName);

                // Кнопка сохранить
                let btnSave = document.createElement(""input"");
                btnSave.type = ""button"";
                btnSave.value = ""Save"";
                btnSave.onclick = function () {
                    let id = document.getElementById(""editNowId"").value;
                    let name = document.getElementById(""editNowName"").value;

                    fetch(""/api/teachers/"" + id, {
                        method: 'PUT',
    ");
            WriteLiteral(@"                    headers: {
                            'Content-Type': 'application/json'
                            // 'Content-Type': 'application/x-www-form-urlencoded',
                        },
                        body: JSON.stringify({
                            id: id,
                            name: name
                        })

                    })
                        .then(fetchGetTeachers)
                        .catch((ex) => { // обрабатываем возможную ошибку
                            console.log(""Error: "" + ex.message);
                            console.log(""Response: "" + ex.response);
                        });
                }
                this.appendChild(btnSave);

                let btnCancel = document.createElement(""input"");
                btnCancel.type = ""button"";
                btnCancel.value = ""Cancel""
                btnCancel.onclick = function () {
                    fetchGetTeachers();
                }
                t");
            WriteLiteral(@"his.appendChild(btnCancel);
            }
            let span = document.createElement(""span"");
            span.innerText = "" del "";
            span.className = json[i].id;
            span.onclick = function () {
                fetch(""/api/Teachers/"" + this.className, {
                    method: 'DELETE'
                })
                    .then(fetchGetTeachers) // Преобразуем ответ в json
                    .catch((ex) => { // обрабатываем возможную ошибку
                        console.log(""Error: "" + ex.message);
                        console.log(""Response: "" + ex.response);
                    });
            }
            li.appendChild(span);
            ul.appendChild(li);
        }

        //Внедряю коллекцию в нужный section
        mainTeachers.appendChild(ul);
    }



    // Выполнение запроса к серверу
    function fetchGetTeachers() {
        fetch(""/api/Teachers"") // Пошлем запрос GET (по умолчанию) по марштуру на сервер
            .then(response => ");
            WriteLiteral(@"response.json()) // Преобразуем ответ в json
            .then(json => renderTeachersList(json)) // Передадим данные в метод
            .catch((ex) => { // обрабатываем возможную ошибку
                console.log(""Error: "" + ex.message);
                console.log(""Response: "" + ex.response);
            });
    }

    fetchGetTeachers();

</script>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591