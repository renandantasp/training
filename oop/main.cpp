#include<iostream>

class Node {
  public:
    int data;
    Node* next;


    Node() : data(0), next(nullptr) {}
    Node(const int _data) : data(_data), next(nullptr) {}

    Node operator+(Node& node_b){
      return (data + node_b.data);
    }

};

class LinkedList {
  private:
    Node* head;
    Node* tail;
  public:
    LinkedList() : head(nullptr), tail(nullptr) {};

    void insert_begin(const int value){
      Node* n = new Node(value);
      if (head == nullptr){
        head = n;
        tail = n;
        return;
      }
      n->next = head;
      head = n;
    }

    void display(){
      Node* current = head;

      if(current == nullptr){
        std::cout << "This structure is empty" << std::endl;
      }
      
      while(current != nullptr){
        std::cout << current->data << " ";
        current = current->next;
      } 
      std::cout << std::endl;
    }

    void insert_after(const int value, const int after){
      Node* current = head;

      while(current != nullptr && current->data != after){
        current = current->next;
      }
      if(current == nullptr){
        std::cout << "The value " << after << " isn't inside the linked list." << std::endl;
        return;
      }
      Node* n = new Node(value);
      n->next = current->next;
      current->next = n;
    }

    void insert_end(const int value){
      Node* current = tail;
      Node* n = new Node(value);
      if (tail == nullptr){
        head = n;
        tail = n;
        return;
      }
      current->next = n;
      tail = n;
    }

    void delete_val(const int value){
      Node* current = head;
      Node* prev = nullptr;
      while(current != nullptr && current->data != value){
        prev = current;
        current = current->next;
      }
      if(current == nullptr){
        std::cout << "The value " << value << "isn't inside the linked list." << std::endl;
        return;
      }
      prev->next = current->next;
      free(current);
    }

    int remove_begin(){
      if(head == nullptr){
        std::cout << "This structure is empty" << std::endl;
        tail = nullptr;
        return -1;
      }

      Node* to_delete = head;
      head = head->next;
      int value = to_delete->data;
      delete to_delete;
      return value;
    }
    

};

class Stack {
  private:
    LinkedList list;
  public:
    
    void push(const int value){
      list.insert_begin(value);
    }

    int pop(){
      return list.remove_begin();
    }

    void display(){
      list.display();
    }
    
};

class Queue {
  private:
    LinkedList list;
  public:
    
    void enqueue(const int value){
      list.insert_end(value);
    }

    int dequeue(){
      return list.remove_begin();
    }

    void display(){
      list.display();
    }

};

int main(){
  
  std::cout<< "----LinkedList----" << std::endl;
  
  LinkedList list;
  list.insert_begin(2);
  list.insert_begin(1);
  list.insert_begin(0);
  list.insert_end(3);
  list.insert_end(4);
  list.remove_begin();
  list.remove_begin();
  list.display();

  std::cout<< "---------------------" << std::endl << std::endl;
  std::cout<< "------- Stack -------" << std::endl;

  Stack s;
  s.push(1);
  s.push(2);
  s.push(3);
  s.push(4);
  s.push(5);
  std::cout << "Value popped: " << s.pop() << std::endl;
  std::cout << "Value popped: " << s.pop() << std::endl;
  std::cout << "Value popped: " << s.pop() << std::endl;
  
  s.display();

  std::cout<< "---------------------" << std::endl << std::endl;
  std::cout<< "------- Queue -------" << std::endl;

  Queue q;
  q.enqueue(1);
  q.enqueue(2);
  q.enqueue(3);
  q.enqueue(4);
  q.enqueue(5);
  std::cout << "Value dequeued: " << q.dequeue() << std::endl;
  std::cout << "Value dequeued: " << q.dequeue() << std::endl;
  std::cout << "Value dequeued: " << q.dequeue() << std::endl;

  q.display();

  std::cout<< "---------------------" << std::endl << std::endl;


}