﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Evangelism
{
    
    public class TextAnalysisLocalClient
    {

        public static string[] Pos, Neg;

        public double Sensitivity { get; set; } = 20;
        public double Bias { get; set; } = 0;

        public TextAnalysisLocalClient()
        {
            Pos = _pos.Split('\n', '\r');
            Neg = _neg.Split('\n', '\r');
        }

        public double AnalyzeSentiment(string text, string lang = "en")
        {
            var T = new TextAnalysisDocumentStore(new TextAnalysisDocument("id",lang,text));
            var R = AnalyzeSentiment(T);
            return R.documents[0].score;
        }

        public TextAnalysisDocumentStore AnalyzeSentiment(TextAnalysisDocumentStore S)
        {
            TextAnalysisDocumentStore res = new TextAnalysisDocumentStore();
            foreach(var d in S.documents)
            {
                var doc = new TextAnalysisDocument(d.id, d.language, d.text);
                doc.score = Analyze(d.text);
                res.documents.Add(doc);
            }
            return res;
        }

        public double Analyze(string s)
        {
            var score = 0;
            var wc = 0;
            foreach(var w in s.Split('\n','\r','-','=','(',')',' ','\t','!','.',',','?','"','\''))
            {
                var ww = w.ToLower();
                if (Pos.Contains(ww)) score++;
                if (Neg.Contains(ww)) score--;
                wc++;
            }
            return 1 / (1 + Math.Exp(-Sensitivity * (score+Bias) / wc));
        }

        public static string _pos =
@"absolutely
adorable
accepted
acclaimed
accomplish
accomplishment
achievement
action
active
admire
adventure
affirmative
affluent
agree
agreeable
amazing
angelic
appealing
approve
aptitude
attractive
awesome
beaming
beautiful
believe
beneficial
bliss
bountiful
bounty
brave
bravo
brilliant
bubbly
calm
celebrated
certain
champ
champion
charming
cheery
choice
classic
classical
clean
commend
composed
congratulation
constant
cool
courageous
creative
cute
dazzling
delight
delightful
distinguished
divine
earnest
easy
ecstatic
effective
effervescent
efficient
effortless
electrifying
elegant
enchanting
encouraging
endorsed
energetic
energized
engaging
enthusiastic
essential
esteemed
ethical
excellent
exciting
exquisite
fabulous
fair
familiar
famous
fantastic
favorable
fetching
fine
fitting
flourishing
fortunate
free
fresh
friendly
fun
funny
generous
genius
genuine
giving
glamorous
glowing
good
gorgeous
graceful
great
green
grin
growing
handsome
happy
harmonious
healing
healthy
hearty
heavenly
honest
honorable
honored
hug
idea
ideal
imaginative
imagine
impressive
independent
innovate
innovative
instant
instantaneous
instinctive
intuitive
intellectual
intelligent
inventive
jovial
joy
jubilant
keen
kind
knowing
knowledgeable
laugh
legendary
light
learned
lively
lovely
lucid
lucky
luminous
marvelous
masterful
meaningful
merit
meritorious
miraculous
motivating
moving
natural
nice
novel
now
nurturing
nutritious
okay
one
one-hundred percent
open
optimistic
paradise
perfect
phenomenal
pleasurable
plentiful
pleasant
poised
polished
popular
positive
powerful
prepared
pretty
principled
productive
progress
prominent
protected
proud
quality
quick
quiet
ready
reassuring
refined
refreshing
rejoice
reliable
remarkable
resounding
respected
restored
reward
rewarding
right
robust
safe
satisfactory
secure
seemly
simple
skilled
skillful
smile
soulful
sparkling
special
spirited
spiritual
stirring
stupendous
stunning
success
successful
sunny
super
superb
supporting
surprising
terrific
thorough
thrilling
thriving
tops
tranquil
transforming
transformative
trusting
truthful
unreal
unwavering
up
upbeat
upright
upstanding
valued
vibrant
victorious
victory
vigorous
virtuous
vital
vivacious
wealthy
welcome
well
whole
wholesome
willing
wonderful
wondrous
worthy
wow
yes
yummy
zeal
zealous";

        public static string _neg =
@"abysmal
adverse
alarming
angry
annoy
anxious
apathy
appalling
atrocious
awful
bad
banal
barbed
belligerent
bemoan
beneath
boring
broken
callous
can't
clumsy
coarse
cold
cold-hearted
collapse
confused
contradictory
contrary
corrosive
corrupt
crazy
creepy
criminal
cruel
cry
cutting
dead
decaying
damage
damaging
dastardly
deplorable
depressed
deprived
deformed
deny
despicable
detrimental
dirty
disease
disgusting
disheveled
dishonest
dishonorable
dismal
distress
don't
dreadful
dreary
enraged
eroding
evil
fail
faulty
fear
feeble
fight
filthy
foul
frighten
frightful
gawky
ghastly
grave
greed
grim
grimace
gross
grotesque
gruesome
guilty
haggard
hard
hard-hearted
harmful
hate
hideous
homely
horrendous
horrible
hostile
hurt
hurtful
icky
ignore
ignorant
ill
immature
imperfect
impossible
inane
inelegant
infernal
injure
injurious
insane
insidious
insipid
jealous
junky
lose
lousy
lumpy
malicious
mean
menacing
messy
misshapen
missing
misunderstood
moan
moldy
monstrous
naive
nasty
naughty
negate
negative
never
no
nobody
nondescript
nonsense
not
noxious
objectionable
odious
offensive
old
oppressive
pain
perturb
pessimistic
petty
plain
poisonous
poor
prejudice
questionable
quirky
quit
reject
renege
repellant
reptilian
repulsive
repugnant
revenge
revolting
rocky
rotten
rude
ruthless
sad
savage
scare
scary
scream
severe
shoddy
shocking
sick
sickening
sinister
slimy
smelly
sobbing
sorry
spiteful
sticky
stinky
stormy
stressful
stuck
stupid
substandard
suspect
suspicious
tense
terrible
terrifying
threatening
ugly
undermine
unfair
unfavorable
unhappy
unhealthy
unjust
unlucky
unpleasant
upset
unsatisfactory
unsightly
untoward
unwanted
unwelcome
unwholesome
unwieldy
unwise
upset
vice
vicious
vile
villainous
vindictive
wary
weary
wicked
woeful
worthless
wound
yell
yucky
zero";
    }
}
