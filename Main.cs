using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPISelection_Multiple
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;//создаем переменную, таким образом как будто заходим в приложение Revit
            UIDocument uidoc = uiapp.ActiveUIDocument;//добираемся до свойства класса UIDocument,
            //обращаемся к переменной uiapp и забираем ActiveUIDocument т.е интерфейс текущего документа
            Document doc = uidoc.Document;//обращаемся к своему документу, к его базе данных

            //вызываем метод, который позволит выбрать элемент с помощью данного приложения
            //Метод PickObject возвращает список, возвращается список элементов типа Reference (ссылки)
            IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, "Выберите элементы");
                        
            var elementList = new List<Element>();//создаем пустой список

            //создаем цикл в котором будем проходится по каждому выбранному элементу в данном списке и приобразовывать его в тип Element
            foreach (var selectedElement in selectedElementRefList)
            {
                Element element = doc.GetElement(selectedElement); //создаем переменную типа Element, вызываем метод GetElement
                elementList.Add(element);//добавляем его в список elementList
             
            }

            TaskDialog.Show("Selection", $"Количество: {elementList.Count}");//выводим количество элементов (Count-считать)


            return Result.Succeeded;
        }
    }
}
