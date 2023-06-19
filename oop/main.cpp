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
  public:
    LinkedList() : head(nullptr) {};

    void insert_begin(const int value){
      Node* n = new Node(value);
      n->next = head;
      head = n;
    }

    void display(){
      Node* current = head;
      
      while(current != nullptr){
        std::cout << current->data << " ";
        current = current->next;
      } 
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
      Node* current = head;
      Node* n = new Node(value);
      while(current->next != nullptr){
        current = current->next;
      }
      current->next = n;
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

    

};

class Stack {
  private:
    Node* top;
  public:
    Stack() : top(nullptr){};
    
    void push(const int value){
      Node* n = new Node(value);
      n->next = top;
      top = n;
    }

    int pop(){
      if(top == nullptr){
        std::cout << "Empty stack" << std::endl;
        return -1;
      }
      Node* pop = top;
      top = top->next;
      int value = pop->data;
      free(pop);
      return value;
    }
};

class Queue {
  private:
    Node* front;
    Node* back;
  public:
    Queue() : front(nullptr), back(nullptr) {};
    
    void enqueue(const int value){
      Node* n = new Node(value);
      if (front == nullptr){
        front = n;
        back = n;
        return;
      }
      Node* current = back;
      current->next = n;
      back = n;
    }

    int dequeue(){
      if(front == nullptr){
        std::cout << "Empty queue" << std::endl;
        return -1;
      }

      Node* deq = front;
      front = front->next;
      if (front == nullptr){
        back = nullptr;
      }
      int value = deq->data;
      free(deq);
      return value;
    }

};

int main(){
  LinkedList list;
  list.insert_begin(4);
  list.insert_begin(3);
  list.insert_begin(1);
  list.insert_begin(0);
  list.insert_after(2,1);
  list.insert_end(5);
  list.delete_val(5);
  list.display();

  std::cout<< std::endl;

  Stack s;
  s.push(3);
  s.push(2);
  s.push(1);
  std::cout << s.pop() << " ";
  std::cout << s.pop() << " ";
  std::cout << s.pop() << " ";
  
  std::cout<< std::endl;

  Queue q;
  q.enqueue(1);
  q.enqueue(2);
  q.enqueue(3);
  std::cout << q.dequeue() << " ";
  std::cout << q.dequeue() << " ";
  std::cout << q.dequeue() << " ";


}