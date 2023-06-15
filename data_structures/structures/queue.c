#include "queue.h"

void initialize_queue(Queue* q){
  q->start = NULL;
  q->end = NULL;
}


void enqueue(Queue* q, int value){
  Node* new_node = create_node(value);

  if(q->start == NULL){
    q->start = new_node;
    q->end = new_node;
    return;
  }

  q->end->next = new_node;
  q->end = new_node;
  
}

int dequeue(Queue* q){
  if (q->end == NULL){
    puts("Queue is empty");
    return -1;
  }

  Node* tmp = q->start;
  int ret = tmp->value;
  q->start = q->start->next;
  free(tmp);
  if(q->start == NULL)
    q->end = NULL;
  return ret;
}

int peek_q(Queue* q){
  if (q->end == NULL){
    puts("Queue is empty");
    return -1;
  }

  return q->start->value;
}