#!/bin/bash
#
# Sets the dev environment

# Project root
ROOT=`pwd`
export ROOT

# Third Party libraries
LIBS=$ROOT/lib

export LD_LIBRARY_PATH=$LD_LIBRARY_PATH:$LIBS
