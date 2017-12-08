// GuessingGameC101142747.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <cstdlib>;
#include <iostream>;
#include <time.h>;
#include <string>;

using namespace std;

int main()
{
	srand(time(NULL));
	int number;
	int randNum = rand() % 100+1;
	bool isPlayer1;
	string player1, player2, currentPlayer;
	string loop = "y";

	cout << "Pleb 1 enter your name: ";
	cin >> player1;
	cout << "Pleb 2 enter your name: ";
	cin >> player2;

	if(randNum <= 50)
	{
		currentPlayer = player1;
	}
	else currentPlayer = player2;
	//cout << currentPlayer << " ,give me your bravest call, pleb: ";
	//cin >> number;
	while(loop == "y")
	{
		do
		{
			cout << currentPlayer << ", give me your bravest call, pleb: ";
			cin >> number;
			if(number < randNum)
			{
				cout << currentPlayer << ", it's too low for my ambitions. Try again" << endl;
			}
			else if(number > randNum)
			{
				cout << currentPlayer << ", that's too high even for my ambitions. Try again." << endl;
			}
			if (currentPlayer == player1)
				currentPlayer = player2;
			else
				currentPlayer = player1;
		}
		while (number != randNum);

		cout << currentPlayer << ", you got it right. This time..." << endl;
		cout << "Okay plebs, want to play again with mastermind like me?   y/n" << endl;
		cin >> loop;
		//system("Pause");
	}

    return 0;
}

