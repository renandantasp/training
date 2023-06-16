#include"bst.h"


Tnode* create_node(int value){
  Tnode* new_node = (Tnode*) malloc(sizeof(Tnode));
  if (new_node == NULL){
    puts("Memory allocation failed.\nExiting the program");
    exit(1);
  }
  new_node->data = value;
  new_node->left = NULL;
  new_node->right = NULL;
  return new_node;
}

Tnode* min_node(Tnode* node) {
    Tnode* tmp = node;
    while (tmp && tmp->left != NULL) {
        tmp = tmp->left;
    }
    return tmp;
}

void insert_node(Tnode** rootptr, int value){
  Tnode* root = *rootptr;
  if(root == NULL){
    *rootptr = create_node(value);
    return;
  }

  if(value < root->data)
    return insert_node(&(root->left), value);
  else if (value > root->data)
    return insert_node(&(root->right), value);

}

int search_node(Tnode** rootptr, int value){
  Tnode* root = *rootptr;
  if(root == NULL) return -1;
  if(root->data == value) return 1;

  if(value < root->data)
    return search_node(&(root->left), value);
  
  return search_node(&(root->left), value);

}

void remove_node(Tnode** rootptr, int data) {
    Tnode* root = *rootptr;
    if (root == NULL) {
        return;
    }
    if (data < (root)->data) {
        remove_node(&(root)->left, data);
    } else if (data > (root)->data) {
        remove_node(&(root)->right, data);
    } else {
        if (root->left == NULL) {
            Tnode* temp = root;
            *rootptr = root->right;
            free(temp);
        } else if (root->right == NULL) {
            Tnode* temp = root;
            *rootptr = root->left;
            free(temp);
        } else {
            Tnode* temp = min_node(root->right);
            root->data = temp->data;
            remove_node(&(root)->right, temp->data);
        }
    }
}

void preorder_trav(Tnode* root){
  if (root == NULL) return;
  printf("%d ", root->data);
  preorder_trav(root->left);
  preorder_trav(root->right);
}

void inorder_trav(Tnode* root){
  if (root == NULL) return;
  inorder_trav(root->left);
  printf("%d ", root->data);
  inorder_trav(root->right);
}

void postorder_trav(Tnode* root) {
  if (root == NULL) return;
  postorder_trav(root->left);
  postorder_trav(root->right);
  printf("%d ", root->data);
}

void print2DUtil(Tnode* root, int space)
{
    if (root == NULL)
        return;
 
    space += COUNT;
 
    print2DUtil(root->right, space);
 
    printf("\n");
    for (int i = COUNT; i < space; i++)
        printf(" ");
    printf("%d\n", root->data);
 
    print2DUtil(root->left, space);
}
 
void print2D(Tnode* root)
{
    // Pass initial space count as 0
    print2DUtil(root, 0);
}

int get_height(Tnode* root){
  if(root == NULL) return 0;

  int left = get_height(root->left);
  int right = get_height(root->right);

  if (left > right){
    return left+1;
  }
  return right+1;
} 