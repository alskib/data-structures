// Franklin Leung
// Lab 3 - COSC 3319
// Fall 2012

package lab3;

public class Node {
    private Node next = null;
    private Object action;
    
    public Node() {
        
    }
    public Node(Object suc) {
        action = suc;
        next = null;        
    }
    public Node(Object suc, Node nextNode) {
        action = suc;
        next = nextNode;
    }
    public Object getAction() {
        return action;
    }
    public void setAction(Object suc) {
        action = suc;
    }
    public Node getNext() {
        return next;
    }
    public void setNext(Node nextNode) {
        next = nextNode;
    }
}
