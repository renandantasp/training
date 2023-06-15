#include "node.h"

typedef struct Queue{
  Node* start;
  Node* end;
} Queue;

void initialize_queue(Queue* q);
void enqueue(Queue* q, int value);
int dequeue(Queue* q);
int peek_q(Queue* q);