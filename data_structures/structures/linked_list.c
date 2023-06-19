#include"linked_list.h"

void insert_beginning(Node** head, int data){
  printf("Insert %d at the beggining.\n", data);

  Node* new_node = create_node(data);
  new_node->next = *head;
  *head = new_node;
}

void insert_end(Node** head, int data){

  printf("Insert %d at the end.\n", data);

  Node* new_node = create_node(data);
  
  if(*head == NULL){
    *head = new_node;
    return;
  }

  Node* temp = *head;
  while(temp->next != NULL){
    temp = temp->next;
  }
  temp->next = new_node;
}

void delete_node(Node** head, int data){

  printf("Delete node with value %d.\n", data);

  if (*head == NULL){
    puts("List is already empty.");
    return;
  }
  Node* temp = *head;
  Node* prev = NULL;

  if(*head!= NULL && temp->value == data){
    *head = temp->next;
    free(temp);
    return;
  }

  while (temp != NULL && temp->value != data){
    prev = temp;
    temp = temp->next;
  }

  if (temp == NULL){
    printf("Value '%d' not found inside the list", data);
    return;
  }

  prev->next = temp->next;
  free(temp);

}

Node* get_node(Node** head, int data){

  printf("Get node with value %d.\n", data);

  if (*head == NULL){
    puts("List is empty.");
    return NULL;
  }
  Node* temp = *head;

  if(*head!= NULL && temp->value == data){
    return temp;
  }

  while (temp != NULL && temp->value != data){
    temp = temp->next;
  }

  if (temp == NULL){
    printf("Value '%d' not found inside the list", data);
    return NULL;
  }

  return temp;

}

void delete_list(Node** head){
  printf("Delete the linked list.\n");

  if (*head == NULL){
    puts("List already empty");
  }
  Node* to_delete = *head;
  Node* nextNode;
  while(to_delete == NULL) {
    nextNode = to_delete->next;
    free(to_delete);
    to_delete = nextNode;
  }
  *head = NULL;
}

void display_list(Node* head){
  if(head == NULL){
    puts("The Linked list is empty.");
    return;
  } 
  
  Node* temp = head;
  while(temp != NULL){
    printf("[ %d ]->", temp->value);
    temp = temp->next;
  }
  puts("NULL\n");
  free(temp);

}