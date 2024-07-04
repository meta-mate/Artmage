using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternReader
{

    public class Node{
        int value, firstValue;
        List<int> name, bigger, incremented, lesserFromBigger;
        bool hasBigger = false;

        const int leftBracket = int.MinValue,
        rightBracket = int.MaxValue;

        public Node(List<int> name, int value){
            this.name = name;
            bigger = calculateBigger();
            incremented = calculateIncremented();

            this.value = value;
            firstValue = value;
        }

        public Node(List<int> name){
            this.name = name;
            bigger = calculateBigger();
            incremented = calculateIncremented();
        }

        public List<int> getName(){
            return name;
        }

        public List<int> getBigger(){
            return bigger;
        }

        public bool getHasBigger(){
            return hasBigger;
        }

        public List<int> getLesserFromBigger(){
            return lesserFromBigger;
        }

        public List<int> getIncremented(){
            return incremented;
        }

        public List<int> copyName(){
            List<int> r = new List<int>();
            
            for(int i = 0; i < name.Count; i++){
                r.Add(name[i]);
            }

            return r;
        }

        public int getValue(){
            return value;
        }

        public int getFirstValue(){
            return firstValue;
        }

        public void setFirstValue(int firstValue){
            this.firstValue = firstValue;
        }

        public void setValue(int value){
            this.value = value;
            if(firstValue == null){
                firstValue = value;
            }
        }

        string intToString(int x){
            string str = "";
            if(x == leftBracket){
                str += "(";
            }else if(x == rightBracket){
                str += ")";
            }else{
                str += x + ".";
            }
            return str;
        }

        public string nameToString(){
            if(name == null) return null;
            
            string str = "";
            for(int i = name.Count - 1; i >= 0; i--){
                str += intToString(name[i]);
            }
            if(str[str.Length - 1] == '.') str = str.Remove(str.Length - 1);

            //if(hasBigger) str += "hasBigger";

            return str;
        }

        public string nameToString(List<int> name){
            if(name == null) return null;
            
            string str = "";
            for(int i = name.Count - 1; i >= 0; i--){
                str += intToString(name[i]);
            }
            if(str[str.Length - 1] == '.') str = str.Remove(str.Length - 1);

            return str;
        }

        public bool nameEquals(List<int> name){
            if(this.name.Count != name.Count) return false;

            for(int i = 0; i < name.Count; i++){
                if(this.name[i] != name[i]){
                    return false;
                }
            }

            //Debug.Log(nameToString() + " = " + nameToString(name));
            return true;
        }

        List<int> calculateIncremented(){
            List<int> r = copyName();

            r.Insert(1, 1);

            return r;
        }

        List<int> calculateBigger(){
            List<int> r = new List<int>(){1};
            lesserFromBigger = new List<int>(){1};

            hasBigger = false;
            int lastSameIndex = 0;
            for(int i = 2; i < name.Count; i++){
                if(name[i] == name[i - 1]){
                    lastSameIndex = i;
                    lesserFromBigger.Add(name[i]);
                    hasBigger = true;
                }else break;
            }

            for(int i = lastSameIndex + 1; i < name.Count; i++){
                r.Add(name[i]);
                lesserFromBigger.Add(name[i]);
            }

            if(lastSameIndex != 0)r.Insert(1, name[lastSameIndex] + 1);

            return r;
        }

        public bool biggerThan(List<int> name){
            int max = 0;
            for(int i = 0; i < name.Count; i++){
                if(max < name[i]) {
                    max = name[i];
                }
            }

            bool foundSame = false;
            for(int i = 0; i < this.name.Count; i++){
                if(max < this.name[i]) return true;
                
                if(max == this.name[i])foundSame = true;
            }

            if(!foundSame) return false;

            for(; max >= 1; max--){
                int count1 = 0, count2 = 0;
                for(int i = 0; i < name.Count; i++){
                    if(max == name[i])count1++;
                }
                for(int i = 0; i < this.name.Count; i++){
                    if(max == this.name[i])count2++;
                }

                if(count1 > count2) return false;
                if(count1 < count2) return true;
            }

            return false;
        }

        List<int> smaller(int iteration){
            if(name == null) return null;
            if(name.Count == 1 && name[0] == 1) return name;
            if(name[name.Count - 1] > 1){
                name[name.Count - 1]--;
                List<int> tempName = new List<int>(name);
                name[name.Count - 1]++;
                return tempName;
            }

            List<int> r = new List<int>();

            return r;
        }

        public int getLast(){
            return name[name.Count - 1];
        }

    }

    List<Node> nodeList = new List<Node>();
    int patternLength = 0;

    public void reset(){
        nodeList.Clear();
        patternLength = 0;
    }

    int modulate(int x, int elementNumber){
        if(elementNumber < 0) return -1;
        while(x < 0) x += elementNumber;
        return (x + elementNumber) % elementNumber;
    }

    public int interpretation(int elementNumber, int pattern){
        
        int lastChange = 0;

        patternLength++;

        int appendNode(int startIndex, int newValue){
        
            int difference = 0;
            
            if(startIndex < nodeList.Count){
                difference = modulate(newValue - nodeList[startIndex].getValue(), elementNumber); 
                nodeList[startIndex].setValue(newValue);
                newValue = difference;

                startIndex++;
            }

            int tempIndex = startIndex;

            List<int> nameToLook = new List<int>{1};

            for(int i = startIndex; i < nodeList.Count; i++){
                if(nodeList[i].nameEquals(nameToLook)){
                    tempIndex = i + 1;

                    difference = modulate(newValue - nodeList[i].getValue(), elementNumber); 
                    nodeList[i].setValue(newValue);
                    newValue = difference;

                }else{
                    break;
                }
            }

            if(tempIndex < nodeList.Count){
                nodeList.Insert(tempIndex, new Node(new List<int>(){1}, newValue));
            }else if(nodeList.Count > 0){
                nodeList.Add(new Node(new List<int>(){1}, newValue));
            }else{
                nodeList.Add(new Node(new List<int>(){0}, newValue));
            }

            return tempIndex;
        }

        int lastNotZeroIndex = 0;

        int tempIndex = appendNode(0, modulate(pattern, elementNumber));

        while(true){
                
            bool isThereSameBelow = false;
            int lastValueDifference = 0;
            List<int> nameToLook = nodeList[tempIndex].getName();

            for(int i = tempIndex - 1; i >= 0; i--){
                if(nodeList[i].biggerThan(nameToLook))
                    break;
                else if(nodeList[i].nameEquals(nameToLook)){
                    isThereSameBelow = true;
                    lastValueDifference = modulate(nodeList[tempIndex].getFirstValue() - nodeList[i].getFirstValue(), elementNumber);
                    break;
                }
            }

            if(nodeList[tempIndex].getValue() != 0)lastNotZeroIndex = tempIndex;

            if(!isThereSameBelow) {
                if(!nodeList[tempIndex].getHasBigger()){
                    lastChange = nodeList[tempIndex].getValue();
                    break;
                }

                nameToLook = nodeList[tempIndex].getLesserFromBigger();

                for(int i = tempIndex - 1; i >= 0; i--){
                    if(nodeList[i].biggerThan(nameToLook))
                        break;
                    else if(nodeList[i].nameEquals(nameToLook)){
                        lastValueDifference = modulate(nodeList[tempIndex].getFirstValue() - nodeList[i].getFirstValue(), elementNumber);
                    }
                }

                nameToLook = nodeList[tempIndex].getBigger();
            }else{
                nameToLook = nodeList[tempIndex].getIncremented();
            }
                
            bool isThereAlready = false;

            int indexToPut = tempIndex + 1;
            for(; indexToPut < nodeList.Count; indexToPut++){
                if(nodeList[indexToPut].biggerThan(nameToLook))
                    break;
                else if(nodeList[indexToPut].nameEquals(nameToLook)){
                    isThereAlready = true;
                    break;
                }    
            }

            if(isThereAlready){
                tempIndex = appendNode(indexToPut, lastValueDifference);
            }else{

                if(indexToPut < nodeList.Count){
                    nodeList.Insert(indexToPut, new Node(nameToLook, lastValueDifference));
                }else{
                    nodeList.Add(new Node(nameToLook, lastValueDifference));
                }
                
                tempIndex = indexToPut;
            }
        }

        
        if(patternLength <= 1) lastChange = 0;
        return lastChange;
    }

    public int interpretation_unused(int elementNumber, int pattern){
        
        int lastChange = 0;

        patternLength++;

        int appendNode(int startIndex, int newValue){
        
            int difference = 0;
            
            if(startIndex < nodeList.Count){
                difference = modulate(newValue - nodeList[startIndex].getValue(), elementNumber); 
                nodeList[startIndex].setValue(newValue);
                newValue = difference;

                startIndex++;
            }

            int tempIndex = startIndex;

            for(int i = startIndex; i < nodeList.Count; i++){
                if(nodeList[i].getLast() == 1){
                    tempIndex = i + 1;

                    difference = modulate(newValue - nodeList[i].getValue(), elementNumber); 
                    nodeList[i].setValue(newValue);
                    newValue = difference;

                }else{
                    break;
                }
            }

            if(tempIndex < nodeList.Count){
                nodeList.Insert(tempIndex, new Node(new List<int>(){1}, newValue));
            }else if(nodeList.Count > 0){
                nodeList.Add(new Node(new List<int>(){1}, newValue));
            }else{
                nodeList.Add(new Node(new List<int>(){0}, newValue));
            }

            return tempIndex;
        }

        int lastNotZeroIndex = 0;

        int tempIndex = appendNode(0, modulate(pattern, elementNumber));

        while(true){
                
            bool isThereSameBelow = false;
            int nameToLook = nodeList[tempIndex].getLast(), lastValueDifference = 0;

            for(int i = tempIndex - 1; i >= 0; i--){
                if(nameToLook < nodeList[i].getLast())
                    break;
                else if(nameToLook == nodeList[i].getLast()){
                    isThereSameBelow = true;
                    lastValueDifference = modulate(nodeList[tempIndex].getFirstValue() - nodeList[i].getFirstValue(), elementNumber);
                    break;
                }
            }

            if(nodeList[tempIndex].getValue() != 0)lastNotZeroIndex = tempIndex;

            if(!isThereSameBelow) {
                lastChange = nodeList[tempIndex].getValue();
                break;
            }
            
            bool isThereAlready = false;
            nameToLook++;

            int indexToPut = tempIndex + 1;
            for(; indexToPut < nodeList.Count; indexToPut++){
                if(nameToLook < nodeList[indexToPut].getLast())
                    break;
                else if(nameToLook == nodeList[indexToPut].getLast()){
                    isThereAlready = true;
                    break;
                }    
            }

            if(isThereAlready){
                tempIndex = appendNode(indexToPut, lastValueDifference);
            }else{

                if(indexToPut < nodeList.Count){
                    nodeList.Insert(indexToPut, new Node(new List<int>(){nameToLook}, lastValueDifference));
                }else{
                    nodeList.Add(new Node(new List<int>(){nameToLook}, lastValueDifference));
                }
                
                tempIndex = indexToPut;
            }
        }

        
        if(patternLength <= 1) lastChange = 0;
        return lastChange;
    }

    public PatternReader copy(){
        PatternReader r = new PatternReader();
        r.setNodeList(getNodeListCopy());
        r.setPatternLength(patternLength);
        return r;
    }

    public int predict(int elementNumber, bool continueExpected){
        List<Node> tempNodeList = getNodeListCopy();
        int tempPatternLength = patternLength;
        int lastChange = interpretation(elementNumber, 0);
        int prediction = modulate(-lastChange, elementNumber);
        
        nodeList.Clear();
        nodeList = tempNodeList;
        patternLength = tempPatternLength;
        
        if(continueExpected){
            interpretation(elementNumber, prediction);
        }
        
        return prediction;
    }
    
    public float score(){
        int maxName = 1;
        float weight = 1, maxScore = 0, score = 0;

        for(int i = 0; i < nodeList.Count; i++){
            weight = 1;
            if(nodeList[i].getFirstValue() != 0){
                score += weight;
            }
            maxScore += weight;
        }

        return score/maxScore;
    }

    public string toStringNodeList(){
        string str = "";
        int oneLengthCounter = 0;
        for(int i = 0; i < nodeList.Count; i++){
            if(nodeList[i].getName().Count > 1){
                str += oneLengthCounter + " ";
                str += "[" + nodeList[i].nameToString() + ", " + nodeList[i].getValue() + "] ";
                oneLengthCounter = 0;
            }else oneLengthCounter++;
        }

        if(oneLengthCounter > 0) str += oneLengthCounter + " ";

        return str;
    }

    public int getPatternLength(){
        return patternLength;
    }

    public int getNodeListCount(){
        return nodeList.Count;
    }

    public void setPatternLength(int patternLength){
        this.patternLength = patternLength;
    }

    public List<Node> getNodeListCopy(){
        List<Node> tempNodeList = new List<Node>();
        for(int i = 0; i < nodeList.Count; i++){
            
            tempNodeList.Add(new Node(nodeList[i].copyName(), nodeList[i].getValue()));
            tempNodeList[i].setFirstValue(nodeList[i].getFirstValue());
        }
        return tempNodeList;
    }

    public void setNodeList(List<Node> nodeList){
        this.nodeList = nodeList;
    }
}
