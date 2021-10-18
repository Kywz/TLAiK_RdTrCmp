using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace TranslatorCompilator
{
    //@TODO Сделать проверку на скобки (не работает) Est'
    //Загрузка/сохранение кода через менюстрип
    //Добавление значений в таблицу 

    class Scanner
    {
        public string scannerMainAlgorith(string currentLine)
        {
            currentLine = currentLine.Trim();
            string returnLine;
            int counterOneForCycles = 0;
            int counterTwoForCycles = 0;
            string bufferString;



            //Цикл на поиск типа данных //POMEN'AT MESTA
            if (currentLine.Length <= 5)
            {
                return "NaF"; // NaF - not a function
            }
            else if (currentLine.Length > 5)
            {
                bufferString = currentLine;
                bufferString = bufferString.Replace("(", "");
                bufferString = bufferString.Replace(")", "");
                if (bufferString.Length+2 != currentLine.Length)
                {
                    return "NaF";
                }
                bufferString = currentLine;
                bufferString = bufferString.Replace("=", "");
                if (bufferString.Length != currentLine.Length)
                {
                    return "NaF";
                }
                if (currentLine[0] == '/' && currentLine[1] == '/')
                {
                    return "NaF";
                }
                
                if (currentLine.Substring(0, 4).Equals("else") || currentLine.Substring(0, 7).Equals("foreach"))
                {
                    return "NaF";
                }
                for(int i = 0; i <=currentLine.Length-1; i++)
                {
                    if (currentLine[i] == ' ' && currentLine[i+1] != ' ')
                    {
                        counterOneForCycles++;
                        //return "NaF";
                    }
                    if (currentLine[i] == '(' && counterOneForCycles < 1 && currentLine[i-1] != ' ')
                    {
                        return "NaF";
                    }
                }
            }
            counterOneForCycles = 0;
            foreach (char currentChar in currentLine)
            {
                counterOneForCycles++;
                if (currentChar == ' ')
                {
                    break;
                }
                
            }
            //Console.WriteLine(currentLine.Substring(0, counterOneForCycles) + " " + counterOneForCycles + ":" + counterTwoForCycles); //DEBUG
            returnLine = currentLine.Substring(0, counterOneForCycles);

            //Цикл на поиск имени
            //counterOneForCycles++; // if [count1] == ' ' ==> [count1]_Next
            counterTwoForCycles = counterOneForCycles;
            for (int currentChar = counterOneForCycles; currentChar < currentLine.Length; currentChar++)
            {
                counterTwoForCycles = currentChar;
                if (currentLine[currentChar] == ' ' || currentLine[currentChar] == '(')
                {
                    break;
                }
            }

           // Console.WriteLine("/n/n/n" + counterTwoForCycles + "/n/n/n");

            if (counterTwoForCycles >= currentLine.Length) //check for any errors 
            {
                return "NaF";
            }
            if (currentLine[counterTwoForCycles] == ';') //(currentLine[counterTwoForCycles - 1] != ' ' && currentLine[counterTwoForCycles - 1] != '(') || 
            {
                return "NaF";
            }
            returnLine = returnLine + ":" + currentLine.Substring(counterOneForCycles-1, counterTwoForCycles - counterOneForCycles + 1); //1 - 1, 2 - +1

            //Цикл на кол-во переменных
            int argumentCounter = 0;
            bufferString = currentLine.Substring(counterTwoForCycles, currentLine.Length - counterTwoForCycles);
            bufferString = bufferString.Replace(" ", String.Empty);
 
            for (int currentChar = 0; currentChar < bufferString.Length; currentChar++)
            {
                if (bufferString[currentChar] == '(' && bufferString[currentChar+1] != ')')
                {
                    argumentCounter++;
                }
                if (bufferString[currentChar] == ')')
                {
                    break;
                }
                if (bufferString[currentChar] == ',' && (bufferString[currentChar + 1] != ')' 
                    || bufferString[currentChar + 1] != ',' || bufferString[currentChar - 1] != ',' 
                    || bufferString[currentChar - 1] != '('))
                {
                    argumentCounter++;
                }
                else if ((bufferString[currentChar] == '(' && bufferString[currentChar + 1] == ')'))
                {
                    break;
                }
                /*else {
                    return "NaF";
                }*/
                counterTwoForCycles = currentChar;
            }
            //Console.WriteLine(returnLine + ":" + argumentCounter + " " + counterOneForCycles + ":" + counterTwoForCycles); //DEBUG
            returnLine = returnLine + ":" + argumentCounter;

            // Алгоритм проверки инициализация/вызова/неизвестности
            currentLine = currentLine.Trim();
            if (currentLine[currentLine.Length-1] == ';')
            {
                returnLine = returnLine + ":C"; // C - call
            }
            else if (currentLine[currentLine.Length-1] == '{')
            {
                returnLine = returnLine + ":I"; // I - Initiallize
            }
            else
            {
                returnLine = returnLine + ":U"; // U - Unknown
            }
            returnLine = returnLine.Replace(" ", "");
            //Console.WriteLine(returnLine + " " + counterOneForCycles + ":" + counterTwoForCycles); //DEBUG
            return returnLine;
        }
        
    }
}
