/* Markov Chain Montre Carlo:
	Ah MCMC! the classic technique to solve the problem of sampling from a complicated distribution.
	Things show up when trying to read about MCMC methods, which are over-complicated and make our lives wee bit more difficult.
	
	What is MCMC :
		MCMC, in lay man terms is a technique to solve the problem of sampling from a complicated distribution.
		This can be explained better through an example (learnt this from my Stat. prof in college)
		
		Lets say we have a magic box that can estimate probabilities of baby names very well. 
		For example, I can give you a string like "HariManjusha" and it will tell you the exact probability PHari that you will choose this name for your next child.
		So there’s a distribution D over all names, it’s very specific to your preferences, and for the sake of argument say this distribution is fixed and you don’t get to tamper with it.
		
		Now comes the problem: I want to efficiently draw a name from this distribution D. 
		This is the problem that Markov Chain Monte Carlo aims to solve. 
		Why is it a problem? Because I have no idea what process you use to pick a name, so I can’t simulate that process myself. 
		Here’s another method you could try: generate a name x uniformly at random, ask the machine for p_x, and then flip a biased coin with probability p_x and use x if the coin lands heads. 
		The problem with this is that there are exponentially many names! 
		The variable here is the number of bits needed to write down a name n = |x|. 
		So either the probabilities p_x will be exponentially small and I’ll be flipping for a very long time to get a single name, 
		or else there will only be a few names with nonzero probability and it will take me exponentially many draws to find them. 
		As my professor always said "Inefficiency is the death of me"
*/

/* PS: The language that I have used as a base to get into programming is good ol' C, and I am in love with it till date.
		It is fast, efficient, and more over it has an excellent scientific library: GSL - GNU scientific library, which includes good random number generation facilities.
*/

#include <stdio.h>
#include <math.h>
#include <stdlib.h>
#include <gsl/gsl_rng.h>
#include <gsl/gsl_randist.h>
 
void main()
{
  int N=20000;
  int thin=500;
  int i,j;
  gsl_rng *r = gsl_rng_alloc(gsl_rng_mt19937);
  double x=0;
  double y=0;
  printf("Iter x y\n");
  for (i=0;i<N;i++) {
    for (j=0;j<thin;j++) {
      x=gsl_ran_gamma(r,3.0,1.0/(y*y+4));
      y=1.0/(x+1)+gsl_ran_gaussian(r,1.0/sqrt(x+1));
    }
    printf("%d %f %f\n",i,x,y);
  }
}

		
	   